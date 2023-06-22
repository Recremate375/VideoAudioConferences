namespace InformationServer.Models
{
	public class Channel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<User> Users { get; set; }
		public List<Message>? MessageHistory { get; set; }
	}
}
