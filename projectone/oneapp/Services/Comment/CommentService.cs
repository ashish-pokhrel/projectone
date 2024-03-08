using AutoMapper;
using oneapp.Entities;
using oneapp.Models;
using oneapp.Repos;
using oneapp.Utilities;

namespace oneapp.Services
{
	public class CommentService : ICommentService
	{
        private readonly ICommentRepo _commentRepo;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepo commentRepo, IMapper mapper)
        {
            _commentRepo = commentRepo;
            _mapper = mapper;
        }

        public async Task<CommentResponse> AddAsync(CommentRequest model)
        {
            ArgumentNullException.ThrowIfNull(model.Description);

            var entity = _mapper.Map<Comment>(model);
            entity.Id = Guid.NewGuid();
            entity.UpdatedOn = SystemHelper.GetCurrentDate();
            entity.UpdatedBy = SystemHelper.GetCurrentUser();
            var result = await _commentRepo.AddAsync(entity);
            return MapCommentResponse(result);
        }

        public async Task<CommentResponse> AddChildCommentAsync(Guid parentCommentId, CommentRequest model)
        {
            ArgumentNullException.ThrowIfNull(parentCommentId);
            ArgumentNullException.ThrowIfNull(model.Description);

            var entity = _mapper.Map<Comment>(model);
            entity.Id = Guid.NewGuid();
            entity.ParentCommentId = parentCommentId;
            entity.UpdatedOn = SystemHelper.GetCurrentDate();
            entity.UpdatedBy = SystemHelper.GetCurrentUser();
            var result = await _commentRepo.AddAsync(entity);
            return MapCommentResponse(result);
        }

        public async Task<CommentListModel> Get(Guid feedId, int skip = 0, int size = 10, string searchValue = "")
        {
            var result = await _commentRepo.Get(feedId, skip, size, searchValue);
            var responseModel = new List<CommentResponse>();
            foreach (var data in result.Item1)
            {
                responseModel.Add(MapCommentResponse(data));
            }
            return new CommentListModel { Count = result.Item2, List = responseModel };
        }

        public async Task<CommentListModel> Get(Guid feedId, Guid parentCommentId, int skip = 0, int size = 10, string searchValue = "")
        {
            var result = await _commentRepo.Get(feedId, parentCommentId, skip, size, searchValue);
            var responseModel = new List<CommentResponse>();
            foreach(var data in result.Item1)
            {
                responseModel.Add(MapCommentResponse(data));
            }
            return new CommentListModel { Count = result.Item2, List = responseModel };
        }

        public async Task<CommentResponse> GetByIdAsync(Guid id)
        {
            var result = await _commentRepo.GetByIdAsync(id);
            return MapCommentResponse(result);
        }

        public async Task<CommentResponse> UpdateLikeAsync(Guid id, int count)
        {
            var comment = await _commentRepo.GetByIdAsync(id);
            comment.TotalLikes += count;

           var result = await _commentRepo.UpdateAsync(comment);
           return MapCommentResponse(result);
        }

        private CommentResponse MapCommentResponse(Comment comment)
        {
            return new CommentResponse
            {
                Id = comment.Id,
                FeedId = comment.FeedId,
                Description = comment.Description,
                ParentCommentId = comment.ParentCommentId,
                ProfileImage = "",
                CommentedByName = "",
                TotalLikes = comment.TotalLikes,
                UpdatedOn = comment.UpdatedOn,
            };
        }
    }
}

