using Microsoft.IdentityModel.Tokens;
using oneapp.Models;
using oneapp.Repos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace oneapp.Services
{
    public class TokenService : ITokenService
    {
        private readonly IUserRepository _userRepository;
        private readonly JWT _jwt;

        public TokenService(IConfiguration config,IUserRepository userRepository)
        {
            _userRepository = userRepository;
           var jwtSection = config.GetSection("JWT");
            _jwt = new JWT
            {
                Audience = config["JWT:Audience"],
                DurationInMinutes = config.GetValue<double>("JWT:DurationInMinutes"),
                Issuer = config["JWT:Issuer"],
                Key = config["JWT:Key"],
            };
        }

        public async Task<AuthenticationModel> GetTokenAsync(string email)
        {
            var authenticationModel = new AuthenticationModel();
            var user = await _userRepository.FindByEmailAsync(email);
            if (user == null)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = $"No Accounts Registered with {email}.";
                return authenticationModel;
            }

            authenticationModel.IsAuthenticated = true;
            var roles = await _userRepository.GetRolesAsync(user);
            authenticationModel.Roles = roles;
            JwtSecurityToken jwtSecurityToken = CreateJwtToken(roles, user);
            authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authenticationModel.Email = user.Email;
            authenticationModel.UserName = user.UserName;
            return authenticationModel;
        }
        private JwtSecurityToken CreateJwtToken(List<string> roles, ApplicationUser user)
        {
            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
    }
}

