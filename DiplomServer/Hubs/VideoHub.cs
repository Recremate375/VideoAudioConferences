using Microsoft.AspNetCore.SignalR;

namespace ConferenceServer.Hubs
{
	public class VideoHub : Hub
	{
		public async Task SendVideo(byte[] videoStream)
		{
			await Clients.All.SendAsync("ReceiveVideo", videoStream);
		}
	}
}
