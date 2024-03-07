using System;
using oneapp.Models;

namespace oneapp.Services
{
    public class CategoryService : ICategoryService
    {
        public Task AddAsync(CategoryRequest entity)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryViewModel> Get(int page = 0, int size = 10, string searchValue = "")
        {
            throw new NotImplementedException();
        }

        public Task<CategoryResponse> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid id, CategoryRequest entity)
        {
            throw new NotImplementedException();
        }
    }
}

