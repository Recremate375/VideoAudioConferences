using InformationServer.Data;
using InformationServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
		[Route("{name:string}")]
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
		[Route("{id: int}")]
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
		public async Task<IActionResult> CreateUser([FromBody] User user)
		{
			await _context.Users.AddAsync(user);
			await _context.SaveChangesAsync();
			return Ok();
		}

	}
}
