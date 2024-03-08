using AutoMapper;
using oneapp.Entities;
using oneapp.Models;
using oneapp.Repos;
using oneapp.Utilities;

namespace oneapp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo _categoryRepo;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepo categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }


        public async Task<CategoryResponse> AddAsync(CategoryRequest model)
        {
            var entity = _mapper.Map<Category>(model);
            entity.Id = Guid.NewGuid();
            entity.ImagePath = "";
            entity.UpdatedOn = SystemHelper.GetCurrentDate();
            entity.UpdatedBy = SystemHelper.GetCurrentUser();
            var result = await _categoryRepo.AddAsync(entity);
            return _mapper.Map<CategoryResponse>(result);
        }

        public async Task<CategoryViewModel> Get(int skip = 0, int size = 10, string searchValue = "")
        {
            var result = await _categoryRepo.Get(skip, size, searchValue);
            var responseModel = _mapper.Map<IEnumerable<CategoryResponse>>(result.Item1);
            return new CategoryViewModel { Count = result.Item2, List = responseModel };
        }

        public async Task<CategoryResponse> GetByIdAsync(Guid id)
        {
            var result = await _categoryRepo.GetByIdAsync(id);
            var response = _mapper.Map<CategoryResponse>(result);
            return response;
        }

        public async Task<CategoryResponse> UpdateAsync(Guid id, CategoryRequest model)
        {
            ArgumentNullException.ThrowIfNull(id);

            var entity = _mapper.Map<Category>(model);
            entity.Id = id;
            entity.ImagePath = "";
            entity.UpdatedOn = SystemHelper.GetCurrentDate();
            entity.UpdatedBy = SystemHelper.GetCurrentUser();
            var result = await _categoryRepo.AddAsync(entity);
            return _mapper.Map<CategoryResponse>(result);
        }
    }
}

