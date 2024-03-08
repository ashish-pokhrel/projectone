using oneapp.Entities;

namespace oneapp.Repos
{
	public interface ICommentRepo
	{
        Task<(IEnumerable<Comment>, int)> Get(Guid feedId, int skip = 0, int size = 10, string searchValue = "");
        Task<(IEnumerable<Comment>, int)> Get(Guid feedId, Guid parentCommentId, int skip = 0, int size = 10, string searchValue = "");
        Task<Comment> AddAsync(Comment entity);
        Task<Comment> UpdateAsync(Comment entity);
        Task<Comment> GetByIdAsync(Guid id);
    }
}

