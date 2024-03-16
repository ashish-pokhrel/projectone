using Microsoft.AspNetCore.Identity;
using oneapp.Entities;
using oneapp.Models;
using oneapp.Models.Auth;
using oneapp.Repos;

namespace oneapp.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterViewModel model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            return await _userRepository.CreateUserAsync(user, model.Password);
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            var user = await _userRepository.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // User not found
                return SignInResult.Failed;
            }

            var isPasswordValid = await _userRepository.CheckPasswordAsync(user, model.Password);
            if (!isPasswordValid)
            {
                // Incorrect password
                return SignInResult.Failed;
            }

            return await _userRepository.SignInAsync(user, model.Password, model.RememberMe);
        }

        public async Task LogoutAsync()
        {
            await _userRepository.SignOutAsync();
        }
    }
}

