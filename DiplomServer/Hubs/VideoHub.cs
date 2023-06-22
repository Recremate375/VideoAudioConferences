using Microsoft.AspNetCore.SignalR;

namespace ConferenceServer.Hubs
{
	public class VideoHub : Hub
	{

		public async Task JoinRoom(string roomId)
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, roomId);

			await Clients.Group(roomId).SendAsync("UserJoined", Context.ConnectionId);
		}

		public async Task LeaveRoom(string roomId)
		{
			await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);

			await Clients.Group(roomId).SendAsync("UserLeft", Context.ConnectionId);
		}

		public async Task SendVideo(byte[] videoStream)
		{
			await Clients.Others.SendAsync("ReceiveVideoAsync", videoStream);
		}
		public async Task SendAudio(byte[] audioStream)
		{
			await Clients.Others.SendAsync("ReceiveAudioAsync", audioStream);
		}
	}
}
