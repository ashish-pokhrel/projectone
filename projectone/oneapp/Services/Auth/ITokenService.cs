using System;
using oneapp.Models;

namespace oneapp.Services
{
    public interface ITokenService
    {
        Task<AuthenticationModel> GetTokenAsync(string email);
    }
}

