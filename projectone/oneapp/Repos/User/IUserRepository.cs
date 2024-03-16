using Microsoft.AspNetCore.Identity;
using oneapp.Entities;
using oneapp.Models.Auth;

namespace oneapp.Repos
{
    public interface IUserRepository
    {
        Task<ApplicationUser> FindByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password);
        Task<SignInResult> SignInAsync(ApplicationUser user, string password, bool rememberMe);
        Task SignOutAsync();
    }
}

