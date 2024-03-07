using System.Data;
using oneapp.Entities;
using oneapp.Repos.DbConnection;

namespace oneapp.Repos
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly DbConnectionFactory _dbConnectionFactory;
        private readonly IDbConnection _dbConnection;

        public CategoryRepo(DbConnectionFactory dbConnectionFactory)
        {
            this._dbConnectionFactory = dbConnectionFactory ?? throw new ArgumentNullException(nameof(dbConnectionFactory));
            this._dbConnection = _dbConnectionFactory.CreateConnection();
        }
        public Task AddAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<Category, int>> Get(int page = 0, int size = 10, string searchValue = "")
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid id, Category entity)
        {
            throw new NotImplementedException();
        }
    }
}

