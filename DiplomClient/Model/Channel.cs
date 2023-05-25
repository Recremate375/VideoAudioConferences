using DiplomClient.Model;
using System.Collections.Generic;
using System.Drawing;

namespace WPFClient.Model
{
	public class Channel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public byte[]? ChannelImage { get; set; }
		public Bitmap channelImage { get; set; }
		public List<User> Users { get; set; }
		public List<Message>? MessageHistory { get; set; }
	}
}
