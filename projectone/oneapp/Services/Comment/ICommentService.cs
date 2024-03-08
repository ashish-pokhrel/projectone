using oneapp.Models;

namespace oneapp.Services
{
	public interface ICommentService
	{
        Task<CommentListModel> Get(Guid feedId, int skip = 0, int size = 10, string searchValue = "");
        Task<CommentListModel> Get(Guid feedId, Guid parentCommentId, int skip = 0, int size = 10, string searchValue = "");
        Task<CommentResponse> AddAsync(CommentRequest model);
        Task<CommentResponse> AddChildCommentAsync(Guid parentCommentId, CommentRequest model);
        Task<CommentResponse> UpdateLikeAsync(Guid id, int count);
        Task<CommentResponse> GetByIdAsync(Guid id);
    }
}

