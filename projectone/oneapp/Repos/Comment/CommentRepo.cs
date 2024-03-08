using Microsoft.EntityFrameworkCore;
using oneapp.Entities;
using oneapp.Repos.DbConnection;

namespace oneapp.Repos
{
	public class CommentRepo : ICommentRepo
	{
        private readonly AppDbContext _context;

        public CommentRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> AddAsync(Comment entity)
        {
            _context.Comment.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<(IEnumerable<Comment>, int)> Get(Guid feedId, int skip = 0, int size = 10, string searchValue = "")
        {
            var query = _context.Comment.AsQueryable();

            if (feedId != Guid.Empty)
            {
                query = query.Where(x => x.FeedId == feedId);
            }

            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                query = query.Where(e => e.Description.Contains(searchValue));
            }

            // Get the total count before paginating
            var totalCount = await query.CountAsync();

            // Paginate the filtered query
            var entities = await query.Skip(skip).Take(size).ToListAsync();

            return (entities, totalCount);
        }

        public async Task<(IEnumerable<Comment>, int)> Get(Guid feedId, Guid parentCommentId, int skip = 0, int size = 10, string searchValue = "")
        {
            var query = _context.Comment.AsQueryable();

            if (feedId != Guid.Empty)
            {
                query = query.Where(x => x.FeedId == feedId);
            }

            if (parentCommentId != Guid.Empty)
            {
                query = query.Where(x => x.ParentCommentId == parentCommentId);
            }

            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                query = query.Where(e => e.Description.Contains(searchValue));
            }

            // Get the total count before paginating
            var totalCount = await query.CountAsync();

            // Paginate the filtered query
            var entities = await query.Skip(skip).Take(size).ToListAsync();

            return (entities, totalCount);
        }

        public async Task<Comment> GetByIdAsync(Guid id)
        {
            return await _context.Comment.FindAsync(id);
        }

        public async Task<Comment> UpdateAsync(Comment entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}

