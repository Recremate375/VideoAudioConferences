using InformationServer.Data;
using InformationServer.DTO;
using InformationServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace InformationServer.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private InformationDbContext _context;
		public UsersController(InformationDbContext dbContext)
		{
			_context = dbContext;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllUsers()
		{
			var Users = await _context.Users.Include(x => x.Channels).ToListAsync();
			return Ok(Users);
		}

		[HttpGet]
		[Route("{name:alpha}")]
		public async Task<IActionResult> GetUser([FromRoute] string Name)
		{
			var user = await _context.Users.Include(x => x.Channels).FirstOrDefaultAsync(x => x.Name == Name);
			if (user != null)
			{
				return Ok(user);
			}

			return BadRequest();
		}

		[HttpGet]
		[Route("{id:int}")]
		public async Task<IActionResult> GetUserChannels([FromRoute]int Id)
		{
			var user = await _context.Users.Include(x => x.Channels).FirstOrDefaultAsync(x => x.Id == Id);
			if (user != null)
			{
				return Ok(user.Channels);
			}
			return BadRequest();
		}

		[HttpPost]
		public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO createUser)
		{
			User user = new User()
			{
				Email = createUser.Email
			};
			await _context.Users.AddAsync(user);
			await _context.SaveChangesAsync();
			return Ok();
		}

		[HttpPut]
		[Route("{id:int}")]
		public async Task<IActionResult> UpdateUser([FromRoute]int id, User user)
		{
			var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

			if(currentUser != null)
			{
				currentUser.Name = user.Name;
				currentUser.Email = user.Email;
				currentUser.Channels = user.Channels;
				await _context.SaveChangesAsync();
				return Ok(currentUser);
			}

			return BadRequest("Can't find a user!");
		}
	}
}
