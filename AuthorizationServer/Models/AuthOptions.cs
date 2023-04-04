using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

namespace AuthorizationServer.Models
{
	public class AuthOptions
	{
		public string Issuer { get; set; }
		public string Audience { get; set; }
		public string Secret { get; set; }
		public int TokenLifetime { get; set; }
		public SymmetricSecurityKey GetSymmetricSecurityKey()
		{

		}
	}
}
