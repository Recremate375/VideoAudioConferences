using AuthorizationServer.Data;
using AuthorizationServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AuthorizationServer.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthorizationController : ControllerBase
	{
		private readonly AuthorizationDbContext _authorizationDbContext;
		private readonly IOptions<AuthOptions> options;


	}
}
