using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using oneapp.Models;
using oneapp.Services;

namespace oneapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }


        // GET: api/values
        [HttpGet]
        public async Task<ActionResult<CategoryViewModel>> Get(int skip = 0, int take = 10, string searchValue = "")
        {
            return await _categoryService.Get(skip, take, searchValue);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResponse>> Get(Guid id)
        {
            return await _categoryService.GetByIdAsync(id);
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<CategoryRequest>> AddAsync([FromForm] CategoryRequest model)
        {
            var response = await _categoryService.AddAsync(model);
            return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryResponse>> Put(Guid id, [FromForm] CategoryRequest model)
        {
            var response = await _categoryService.UpdateAsync(id, model);
            return Ok(response);
        }
    }
}

