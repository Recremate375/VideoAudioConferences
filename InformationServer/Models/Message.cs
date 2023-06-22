namespace InformationServer.Models
{
	public class Message
	{
		public int Id { get; set; }
		public string message { get; set; }
		public DateTime messageDate { get; set; }
		public User user { get; set; }
	}
}
