using Microsoft.AspNetCore.Identity;
using oneapp.Entities;

namespace oneapp.Repos
{
    public class UserRepo : IUserRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UserRepo(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityUser> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> CheckPasswordAsync(IdentityUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IdentityResult> CreateUserAsync(IdentityUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<SignInResult> SignInAsync(IdentityUser user, string password, bool rememberMe)
        {
            return await _signInManager.PasswordSignInAsync(user, password, rememberMe, lockoutOnFailure: false);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }

}

