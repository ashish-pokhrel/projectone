using Microsoft.AspNetCore.Identity;
using oneapp.Models;

namespace oneapp.Services
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterAsync(RegisterViewModel model);
        Task<SignInResult> LoginAsync(LoginViewModel model);
        Task LogoutAsync();
    }
}

