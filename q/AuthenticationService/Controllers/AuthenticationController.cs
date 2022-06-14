using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthenticationService.Data;
using AuthenticationService.Models;
using AuthenticationService.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationService.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthenticationController : ControllerBase
	{
		private AuthenticationContext Context { get; }
		private IPasswordHasher PasswordHasher { get; }
		private readonly IConfiguration _configuration;

		public AuthenticationController(IConfiguration configuration ,AuthenticationContext context, IPasswordHasher passwordHasher)
		{
			_configuration = configuration;
			Context = context;
			PasswordHasher = passwordHasher;
		}

		[HttpGet]
		public string Ping()
		{
			return "Pong";
		}

		[HttpPost]
		public async Task<ActionResult<User>> CreateUserAsync([FromBody] CreateUserRequest request)
		{
            User? existingUser = await Context.Users.FirstOrDefaultAsync(dbUser => dbUser.Email == request.Email);

            if (existingUser is not null)
            {
				return BadRequest("Email is already used");
            }

            
            try{
                User user = new User()
                {
                    Email = request.Email,
                    Password = PasswordHasher.HashPassword(request.Password)
                };

                Context.Users.Add(user);
                await Context.SaveChangesAsync();
				return user;
            }
            
            catch(Exception ex){
	            return BadRequest(ex.Message);
            }
		}

		[HttpPost("login")]
		public async Task<ActionResult<User>> Login([FromBody] LoginRequest request)
		{
			User? existingUser = await Context.Users.FirstOrDefaultAsync(dbUser => dbUser.Email == request.Email);

			if (existingUser is null)
			{
				return BadRequest("User does not exist");
			}

			if(!PasswordHasher.PasswordMatches(existingUser.Password , request.Password))
            {
				return BadRequest("Wrong credentials");
            }

			string token = CreateToken(existingUser);
			return Ok(token);
		}

		private string CreateToken(User user)
        {
			List<Claim> claims = new List<Claim>
			{
				new Claim(ClaimTypes.Email, user.Email)
			};

			var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
				_configuration.GetSection("Appsettings:Token").Value));

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

			var token = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.Now.AddDays(1),
				signingCredentials: creds);

			var jwt = new JwtSecurityTokenHandler().WriteToken(token);

			return jwt;
        }
	}
}