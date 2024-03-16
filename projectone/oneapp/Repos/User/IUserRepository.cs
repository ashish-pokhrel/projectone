using Microsoft.AspNetCore.Identity;
using oneapp.Entities;

namespace oneapp.Repos
{
    public interface IUserRepository
    {
        Task<IdentityUser> FindByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(IdentityUser user, string password);
        Task<IdentityResult> CreateUserAsync(IdentityUser user, string password);
        Task<SignInResult> SignInAsync(IdentityUser user, string password, bool rememberMe);
        Task SignOutAsync();
    }
}

