using AuthorizationServer.Data;
using AuthorizationServer.DTO;
using AuthorizationServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthorizationServer.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		AuthorizationDbContext _context;
		private readonly HttpClient httpClient;
		public AccountController(AuthorizationDbContext context)
		{
			_context = context;
			httpClient = new HttpClient();
		}

		[HttpPost]
		public async Task<IActionResult> Register([FromBody] UserDTO user)
		{
			var findUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email);

			if (findUser != null)
			{
				return BadRequest("This user is already registered!");
			}

			User newUser = new User
			{
				Email = user.Email,
				Password = user.Password
			};

			string url = "https://localhost:7156/api/Users";
			string json = Newtonsoft.Json.JsonConvert.SerializeObject(user);

			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await httpClient.PostAsync(url, content);

			if (response.IsSuccessStatusCode)
			{
				await _context.Users.AddAsync(newUser);
				await _context.SaveChangesAsync();

				return Created($"User {newUser.Email} successfule created", newUser);
			}
			else
			{
				return BadRequest("Can't save user!");
			}
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] UserDTO user)
		{
			var thisUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email);

			if (thisUser != null)
			{
				if (user.Password != thisUser.Password)
				{
					return BadRequest("Incorrect password!");
				}
				var tokenHandler = new JwtSecurityTokenHandler();
				var key = Encoding.ASCII.GetBytes("thissuperSecretKey");
				var tokenDescriptor = new SecurityTokenDescriptor
				{
					Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
					{
						new Claim(ClaimTypes.Name, thisUser.Id.ToString())
					}),
					Expires = DateTime.UtcNow.AddDays(30),
					SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
																SecurityAlgorithms.HmacSha256Signature)
				};

				var token = tokenHandler.CreateToken(tokenDescriptor);
				var tokenString = tokenHandler.WriteToken(token);

				return Ok(new { Token = tokenString });
			}

			return Unauthorized("Person is not found");
		}
	}
}
