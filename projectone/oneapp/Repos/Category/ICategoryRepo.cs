using oneapp.Entities;

namespace oneapp.Repos
{
	public interface ICategoryRepo
	{
        Task<Tuple<Category, int>> Get(int page = 0, int size = 10, string searchValue = "");
        Task AddAsync(Category entity);
        Task UpdateAsync(Guid id, Category entity);
        Task<Category> GetByIdAsync(Guid id);
    }
}

