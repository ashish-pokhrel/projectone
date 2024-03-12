using AutoMapper;
using oneapp.Entities;
using oneapp.Models;
using oneapp.Repos;
using oneapp.Utilities;

namespace oneapp.Services
{
	public class FeedService : IFeedService
	{
        private readonly IFeedRepo _feedRepo;
        private readonly ICategoryRepo _categoryRepo;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IImageHubRepo _imageHubRepo;
        public FeedService(IFeedRepo feedRepo, IMapper mapper, IFileService fileService, IImageHubRepo imageHubRepo, ICategoryRepo categoryRepo)
        {
            _feedRepo = feedRepo;
            _mapper = mapper;
            _fileService = fileService;
            _imageHubRepo = imageHubRepo;
            _categoryRepo = categoryRepo;
        }

        public async Task<FeedResponse> AddAsync(FeedRequest model)
        {
            var imageList = new List<string>();
            var entity = new Feed
            {
                Id = Guid.NewGuid(),
                UpdatedOn = SystemHelper.GetCurrentDate(),
                UpdatedBy = SystemHelper.GetCurrentUser(),
                CreatedOn = SystemHelper.GetCurrentDate(),
                CreatedBy = SystemHelper.GetCurrentUser(),
                CategoryId = model.CategoryId,
                TotalShares = 0,
                TotalLikes = 0,
                Content = model.Content,
                IsActive = true,
                LocationId = Guid.Empty, // TODO
                Tags = model.Tags,
                Title = model.Title
            };
            try
            {
                foreach (var image in model.FeedImages)
                {
                    var fileName = await _fileService.UploadFileAsync(image);
                    imageList.Add(fileName);
                    var imageHubEntity = new ImageHub
                    {
                        Id = Guid.NewGuid(),
                        ImagePath = fileName,
                        IsActive = true,
                        TableId = entity.Id,
                        TableName = "Feed"
                    };
                    await _imageHubRepo.AddAsync(imageHubEntity);
                }

                var result = await _feedRepo.AddAsync(entity);
                return await GetFeedResponseFromEntity(result);
            }
            catch
            {
                imageList.ForEach(async imageName => await _fileService.DeleteFileAsync(imageName));
                throw;
            }
        }

        public async Task<FeedListModel> Get(Guid categoryId,int skip = 0, int size = 10, string searchValue = "")
        {
            var result = await _feedRepo.Get(categoryId, skip, size, searchValue);
            var feeds = new List<FeedResponse>();
            foreach(var data in result.Item1)
            {
                var feed = await GetFeedResponseFromEntity(data);
                feeds.Add(feed);
            }
            return new FeedListModel { Count = result.Item2, List = feeds };
        }

        public async Task<FeedResponse> GetByIdAsync(Guid id)
        {
            var feed = await _feedRepo.GetByIdAsync(id);
            return await GetFeedResponseFromEntity(feed);
        }

        public async Task<FeedResponse> UpdateAsync(Guid id, FeedRequest model)
        {
            var feed = await _feedRepo.GetByIdAsync(id);
            feed.Content = model.Content;
            feed.UpdatedBy = SystemHelper.GetCurrentUser();
            feed.UpdatedOn = SystemHelper.GetCurrentDate();

            await _feedRepo.UpdateAsync(feed);
            return await GetFeedResponseFromEntity(feed);
        }

        public async Task<FeedResponse> UpdateLikeAsync(Guid id, int count)
        {
            var feed = await _feedRepo.GetByIdAsync(id);
            feed.TotalLikes += count;

            await _feedRepo.UpdateAsync(feed);
            return await GetFeedResponseFromEntity(feed);
        }

        public async Task<FeedResponse> UpdateShareAsync(Guid id, int count)
        {
            var feed = await _feedRepo.GetByIdAsync(id);
            feed.TotalShares += count;

            await _feedRepo.UpdateAsync(feed);
            return await GetFeedResponseFromEntity(feed);
        }

        private async Task<FeedResponse> GetFeedResponseFromEntity(Feed data)
        {
            var images = await _imageHubRepo.GetByTableIdAsync(data.Id);
            var category = await _categoryRepo.GetByIdAsync(data.CategoryId);

            return new FeedResponse
            {
                Content = data.Content,
                TotalShares = data.TotalShares,
                CategoryName = category.CategoryName,
                CreatedByName = "",
                CreatedOn = data.CreatedOn,
                Id = data.Id,
                TotalLikes = data.TotalLikes,
                Tags = data.Tags,
                ProfileImage = "", // TODO
                Title = data.Title,
                Images = images.Select(x => x.ImagePath),
            };
        }
    }
}

