using Microsoft.AspNetCore.Mvc;
using oneapp.Models;
using oneapp.Services;

namespace oneapp.Controllers
{
    [Route("api/[controller]")]
    public class FeedController : Controller
    {
        private readonly IFeedService _feedService;

        public FeedController(IFeedService feedService)
        {
            _feedService = feedService ?? throw new ArgumentNullException(nameof(feedService));
        }

        // GET: api/values
        [HttpGet]
        public async Task<ActionResult<FeedListModel>> Get(Guid categoryId,int skip = 0, int take = 10, string searchValue = "")
        {
            return await _feedService.Get(categoryId, skip, take, searchValue);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FeedResponse>> Get(Guid id)
        {
            return await _feedService.GetByIdAsync(id);
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<FeedResponse>> AddAsync([FromForm] FeedRequest model)
        {
            var response = await _feedService.AddAsync(model);
            return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ActionResult<FeedResponse>> Put(Guid id, [FromForm] FeedRequest model)
        {
            var response = await _feedService.UpdateAsync(id, model);
            return Ok(response);
        }

        // PUT api/values/5
        [HttpPut("like/{id}")]
        public async Task<ActionResult<FeedResponse>> Like(Guid id, int count)
        {
            var response = await _feedService.UpdateLikeAsync(id, count);
            return Ok(response);
        }

        // PUT api/values/5
        [HttpPut("share/{id}")]
        public async Task<ActionResult<FeedResponse>> Share(Guid id, int count)
        {
            var response = await _feedService.UpdateShareAsync(id, count);
            return Ok(response);
        }
    }
}

