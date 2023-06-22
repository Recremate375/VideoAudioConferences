using InformationServer.Data;
using InformationServer.DTO;
using InformationServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace InformationServer.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ChannelsController : ControllerBase
	{
		private readonly InformationDbContext _context;

		public ChannelsController(InformationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllChannel()
		{
			var channels = await _context.Channels.Include(x => x.Users).Include(x => x.MessageHistory).ToListAsync();
			return Ok(channels);
		}

		[HttpGet]
		[Route("{id:int}")]
		public async Task<IActionResult> GetChannelMessageHistory([FromRoute]int Id)
		{
			var channel = await _context.Channels.Include(x => x.MessageHistory).FirstOrDefaultAsync(x => x.Id == Id);
			if (channel != null)
			{
				return Ok(channel.MessageHistory);
			}
			return BadRequest("Can't find this channel");
		}

		[HttpPost]
		public async Task<IActionResult> CreateChannel([FromBody]CreateChannelDTO createChannel)
		{
			var user = await _context.Users.Include(x => x.Channels).FirstOrDefaultAsync(x => x.Id == createChannel.UserId);
			if (user != null)
			{
				Channel channel = new()
				{
					Name = createChannel.Name,
					MessageHistory = createChannel.MessageHistory,
					Users = new List<User> { user }
				};

				await _context.Channels.AddAsync(channel);
				await _context.SaveChangesAsync();
				return Ok();
			}
			return BadRequest("Can't create channel");
		}

		[HttpPut]
		[Route("{id:int}")]
		public async Task<IActionResult> UpdateChannel([FromRoute]int id, Channel updateChannel)
		{
			var channel = await _context.Channels.Include(x => x.Users).Include(x => x.MessageHistory).FirstOrDefaultAsync(x => x.Id == id);
			if (channel == null)
			{
				return NotFound();
			}

			channel.MessageHistory = updateChannel.MessageHistory;
			channel.Name = updateChannel.Name;
			channel.Users = updateChannel.Users;

			await _context.SaveChangesAsync();
			return Ok(channel);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteChannel([FromRoute] int id)
		{
			return Ok();
		}
	}
}
