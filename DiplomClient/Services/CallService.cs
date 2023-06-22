using Emgu.CV;
using Emgu.CV.Structure;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace WPFClient.Services
{
	public class CallService
	{
		
		private HubConnection hubConnection;
		public BitmapImage image = new BitmapImage();
		public async Task ConnectAsync()
		{
			hubConnection = new HubConnectionBuilder().WithUrl("https://localhost:7154/videoHub").Build();

			await hubConnection.StartAsync();
		}

		public async Task SendVideoAsync(byte[] frame)
		{
			await hubConnection.SendAsync("SendVideo", frame);
		}
		public async Task ReceiveVideoAsync(byte[] frame)
		{
			image = await ByteArrayToImageAsync(frame);
		}

		private async Task<BitmapImage> ByteArrayToImageAsync(byte[] imageData)
		{
			using (MemoryStream ms = new MemoryStream(imageData))
			{
				BitmapImage img = new BitmapImage();
				img.BeginInit();
				img.StreamSource = ms;
				img.CacheOption = BitmapCacheOption.OnLoad;
				img.EndInit();

				await img.Dispatcher.InvokeAsync(() => { });
				return img;
			}
		}
	}
}
