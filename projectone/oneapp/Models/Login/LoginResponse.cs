using System;
namespace oneapp.Models
{
	public class LoginResponse
	{
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Token { get; set; }
        public string ProfileImage { get; set; }
        public string ExpiryToken { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
