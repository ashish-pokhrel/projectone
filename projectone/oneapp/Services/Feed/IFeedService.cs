using System;
using oneapp.Models;

namespace oneapp.Services
{
	public interface IFeedService
	{
        Task<FeedListModel> Get(Guid categoryId,int skip = 0, int size = 10, string searchValue = "");
        Task<FeedResponse> AddAsync(FeedRequest model);
        Task<FeedResponse> UpdateAsync(Guid id, FeedRequest model);
        Task<FeedResponse> UpdateLikeAsync(Guid id, int count);
        Task<FeedResponse> UpdateShareAsync(Guid id, int count);
        Task<FeedResponse> GetByIdAsync(Guid id);
    }
}

