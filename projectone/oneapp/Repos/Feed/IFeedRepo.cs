using oneapp.Entities;

namespace oneapp.Repos
{
	public interface IFeedRepo
	{
        Task<(IEnumerable<Feed>, int)> Get(Guid categoryId,int skip = 0, int size = 10, string searchValue = "");
        Task<Feed> AddAsync(Feed entity);
        Task<Feed> UpdateAsync(Guid id, Feed entity);
        Task<Feed> GetByIdAsync(Guid id);
    }
}

