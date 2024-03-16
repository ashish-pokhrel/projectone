using Microsoft.EntityFrameworkCore;
using oneapp.Entities;

namespace oneapp.Repos
{
	public class ImageHubRepo : IImageHubRepo
	{
        private readonly AppDbContext _context;

        public ImageHubRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ImageHub> AddAsync(ImageHub entity)
        {
            _context.ImageHub.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<(IEnumerable<ImageHub>, int)> Get(int skip = 0, int size = 10, string searchValue = "")
        {
            var query = _context.ImageHub.AsQueryable();
            var totalCount = await query.CountAsync();
            var entities = await query.Skip(skip).Take(size).ToListAsync();

            return (entities, totalCount);
        }

        public async Task<IEnumerable<ImageHub>> GetByTableIdAsync(Guid tableId)
        {
            var query = _context.ImageHub.AsQueryable();
            return await query.Where(x => x.TableId == tableId).ToListAsync();
        }

        public async Task<ImageHub> UpdateAsync(Guid id, ImageHub entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}

