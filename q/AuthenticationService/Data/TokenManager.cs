using System;
using System.Collections.Generic;
using System.Security.Claims;
using AuthenticationService.Models;

namespace AuthenticationService.Data
{
	public class TokenManager
	{
		public List<Claim> claims = new List<Claim>();
        
		private string CreateToken(User user)
        {
			claims.Add(new Claim(ClaimTypes.Email, user.Email));
			return string.Empty;
        }
	}
}

