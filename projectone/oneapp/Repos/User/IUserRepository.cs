using Microsoft.AspNetCore.Identity;
using oneapp.Models;

namespace oneapp.Repos
{
    public interface IUserRepository
    {
        Task<ApplicationUser> FindByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password);
        Task<SignInResult> SignInAsync(ApplicationUser user, string password, bool rememberMe);
        Task SignOutAsync();
        Task<List<string>> GetRolesAsync(ApplicationUser user);
        Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role);
    }
}

