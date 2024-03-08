using oneapp.Entities;

namespace oneapp.Repos
{
	public interface IImageHubRepo
	{
        Task<(IEnumerable<ImageHub>, int)> Get(int skip = 0, int size = 10, string searchValue = "");
        Task<ImageHub> AddAsync(ImageHub entity);
        Task<ImageHub> UpdateAsync(Guid id, ImageHub entity);
        Task<IEnumerable<ImageHub>> GetByTableIdAsync(Guid tableId);
    }
}

