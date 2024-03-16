using Microsoft.AspNetCore.Identity;
using oneapp.Models;
using oneapp.Repos;
using oneapp.Utilities;
using System.Threading.Tasks;

namespace oneapp.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(IUserRepository userRepository, ITokenService tokenService, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<(SignInResult, AuthenticationModel)> RegisterAsync(RegisterViewModel model, string role)
        {
            if(string.IsNullOrEmpty(role))
            {
                role = RolesConstant.NORMALUSER;
            }

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userRepository.CreateUserAsync(user, model.Password);
            if (result.Succeeded)
            {
                // Assign roles to the user
                await AssignRolesToUser(user, role);

                // Generate token
                var tokenResult = await _tokenService.GetTokenAsync(user.Email);
                return (SignInResult.Success, tokenResult);
            }

            return (SignInResult.Failed, null);
        }

        public async Task<(SignInResult, AuthenticationModel)> LoginAsync(LoginViewModel model)
        {
            var user = await _userRepository.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // User not found
                return (SignInResult.Failed, null);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var tokenResult = await _tokenService.GetTokenAsync(user.Email);
                return (SignInResult.Success, tokenResult);
            }

            return (SignInResult.Failed, null);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        private async Task AssignRolesToUser(ApplicationUser user, string role)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                // Create the role if it doesn't exist
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
            await _userRepository.AddToRoleAsync(user, role);
        }
    }
}
