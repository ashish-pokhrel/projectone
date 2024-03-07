using oneapp.Models;

namespace oneapp.Services
{
	public interface ICategoryService
	{
        Task<CategoryViewModel> Get(int page = 0, int size = 10, string searchValue = "");
        Task AddAsync(CategoryRequest entity);
        Task UpdateAsync(Guid id, CategoryRequest entity);
        Task<CategoryResponse> GetByIdAsync(Guid id);
    }
}

