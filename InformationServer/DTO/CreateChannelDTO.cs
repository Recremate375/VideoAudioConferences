using InformationServer.Models;

namespace InformationServer.DTO
{
	public class CreateChannelDTO
	{
		public string Name { get; set; }
		public int UserId { get; set; }
		public List<Message>? MessageHistory { get; set; }
	}
}
