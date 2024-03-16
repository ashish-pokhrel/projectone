using System.Data;
using Microsoft.EntityFrameworkCore;
using oneapp.Entities;

namespace oneapp.Repos
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly AppDbContext _context;

        public CategoryRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Category> AddAsync(Category entity)
        {

            if (!string.IsNullOrWhiteSpace(entity.CategoryName))
            {
                var hasDuplicate = _context.Category.AsQueryable().Any(e => e.CategoryName.Contains(entity.CategoryName));
                if(hasDuplicate)
                {
                    throw new DuplicateNameException();
                }
            }

            _context.Category.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<(IEnumerable<Category>, int)> Get(int skip = 0, int size = 10, string searchValue = "")
        {
            var query = _context.Category.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                query = query.Where(e => e.CategoryName.Contains(searchValue));
                // Add other filters as needed
            }

            // Get the total count before paginating
            var totalCount = await query.CountAsync();

            // Paginate the filtered query
            var entities = await query.Skip(skip).Take(size).ToListAsync();

            return (entities, totalCount);
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            return await _context.Category.FindAsync(id);
        }

        public async Task<Category> UpdateAsync(Category entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}

