using Microsoft.AspNetCore.Mvc;
using oneapp.Models;

namespace oneapp.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] LoginRequest model)
        {
            return Ok(
                new LoginResponse
                {
                    ExpiryToken = DateTimeOffset.UtcNow.AddHours(2).ToString(),
                    FullName = "Jack",
                    IsAuthenticated = true,
                    ProfileImage = "imagelink",
                    Token = "f1827e78-1a5e-432c-91ca-4136761a1669",
                    UserName = "root"
                });
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

