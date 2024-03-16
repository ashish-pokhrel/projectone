using Microsoft.AspNetCore.Identity;
using oneapp.Models;

namespace oneapp.Services
{
    public interface IAuthService
    {
        Task<(SignInResult, AuthenticationModel)> RegisterAsync(RegisterViewModel model, string role);
        Task<(SignInResult, AuthenticationModel)> LoginAsync(LoginViewModel model);
        Task LogoutAsync();
    }
}

