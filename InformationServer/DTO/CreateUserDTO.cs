using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace InformationServer.DTO
{
	public class CreateUserDTO
	{
		public string Email { get; set; }
		public string? Name { get; set; }
	}
}
