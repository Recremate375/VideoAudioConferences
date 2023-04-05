using Emgu.CV;
using Emgu.CV.Structure;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Services
{
	public class CallService
	{
		
		private HubConnection hubConnection;
		private VideoCapture capture = null;
		public async Task ConnectAsync()
		{
			hubConnection = new HubConnectionBuilder().WithUrl("https://localhost:7154/videoHub").Build();

			await hubConnection.StartAsync();
		}

		public async Task SendVideoAsync(System.IO.Stream videoStream)
		{
			using (var memoryStream = new MemoryStream())
			{
				await videoStream.CopyToAsync(memoryStream);
				await hubConnection.SendAsync("SendVideo", memoryStream.ToArray());
			}
		}
		public async Task ReceiveVideoAsync(byte[] videoStream)
		{
			
		}
	}
}
