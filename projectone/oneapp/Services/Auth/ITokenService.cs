using System;
namespace oneapp.Services
{
    public interface ITokenService
    {
        string GenerateToken(string userId);
    }
}

