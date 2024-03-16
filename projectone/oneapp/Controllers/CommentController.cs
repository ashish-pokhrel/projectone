using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using oneapp.Models;
using oneapp.Services;

namespace oneapp.Controllers
{
    [Authorize(Roles = "Admin, NormalUser")]
    [Route("api/[controller]")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService ?? throw new ArgumentNullException(nameof(commentService));
        }

        // GET: api/values
        [HttpGet]
        public async Task<ActionResult<CommentListModel>> Get(Guid feedId, int skip = 0, int take = 10, string searchValue = "")
        {
            return await _commentService.Get(feedId, skip, take, searchValue);
        }

        [HttpGet("childComments")]
        public async Task<ActionResult<CommentListModel>> Get(Guid feedId, Guid parentCommentId, int skip = 0, int take = 10, string searchValue = "")
        {
            return await _commentService.Get(feedId, parentCommentId, skip, take, searchValue);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentResponse>> Get(Guid id)
        {
            return await _commentService.GetByIdAsync(id);
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<CommentResponse>> AddAsync([FromBody] CommentRequest model)
        {
            var response = await _commentService.AddAsync(model);
            return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
        }

        // POST api/values
        [HttpPost("childComments")]
        public async Task<ActionResult<CommentResponse>> AddAsync([Required]Guid parentCommentId, [FromForm] CommentRequest model)
        {
            var response = await _commentService.AddChildCommentAsync(parentCommentId, model);
            return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
        }

        // PUT api/values/5
        [HttpPut("like/{id}")]
        public async Task<ActionResult<CommentResponse>> Like(Guid id, int count)
        {
            var response = await _commentService.UpdateLikeAsync(id, count);
            return Ok(response);
        }
      
    }
}
