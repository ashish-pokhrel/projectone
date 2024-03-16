using Microsoft.AspNetCore.Mvc;
using oneapp.Models;
using oneapp.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace oneapp.Controllers
{
    // AuthController.cs

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.RegisterAsync(model, string.Empty);

            if (result.Item1.Succeeded)
            {
                return Ok(result.Item2);
            }
            else
            {
                return BadRequest(new { Error = "Registration failed", Errors = result.Item1 });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.LoginAsync(model);

            if (result.Item1.Succeeded)
            {
                return Ok(result.Item2);
            }
            else
            {
                return BadRequest(new { Error = "Registration failed", Errors = result.Item1 });
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return Ok(new { Message = "Logout successful" });
        }
    }

}

