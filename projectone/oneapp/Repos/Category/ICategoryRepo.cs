using oneapp.Entities;
using oneapp.Models;

namespace oneapp.Repos
{
	public interface ICategoryRepo
	{
        Task<(IEnumerable<Category>, int)> Get(int skip = 0, int size = 10, string searchValue = "");
        Task<Category> AddAsync(Category entity);
        Task<Category> UpdateAsync(Category entity);
        Task<Category> GetByIdAsync(Guid id);
    }
}

