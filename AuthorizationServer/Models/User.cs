using Microsoft.Identity.Client;

namespace AuthorizationServer.Models
{
	public class User
	{
		public int Id { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
	}
}
