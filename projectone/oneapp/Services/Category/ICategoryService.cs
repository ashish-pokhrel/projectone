using oneapp.Models;

namespace oneapp.Services
{
	public interface ICategoryService
	{
        Task<CategoryViewModel> Get(int skip = 0, int size = 10, string searchValue = "");
        Task<CategoryResponse> AddAsync(CategoryRequest model);
        Task<CategoryResponse> UpdateAsync(Guid id, CategoryRequest model);
        Task<CategoryResponse> GetByIdAsync(Guid id);
    }
}

