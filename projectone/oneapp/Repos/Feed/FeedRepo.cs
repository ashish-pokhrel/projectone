using System;
using System.Data;
using Microsoft.EntityFrameworkCore;
using oneapp.Entities;
using oneapp.Repos.DbConnection;

namespace oneapp.Repos
{
	public class FeedRepo : IFeedRepo
	{
        private readonly AppDbContext _context;

        public FeedRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Feed> AddAsync(Feed entity)
        {
            _context.Feed.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<(IEnumerable<Feed>, int)> Get(Guid categoryId, int skip = 0, int size = 10, string searchValue = "")
        {
            var query = _context.Feed.AsQueryable();

            if(categoryId != Guid.Empty)
            {
                query = query.Where(x => x.CategoryId == categoryId);
            }

            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                query = query.Where(e => e.Title.Contains(searchValue)
                || e.Content.Contains(searchValue));
            }

            // Get the total count before paginating
            var totalCount = await query.CountAsync();

            // Paginate the filtered query
            var entities = await query.Skip(skip).Take(size).ToListAsync();

            return (entities, totalCount);
        }

        public async Task<Feed> GetByIdAsync(Guid id)
        {
            return await _context.Feed.FindAsync(id);
        }

        public async Task<Feed> UpdateAsync(Feed entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}

