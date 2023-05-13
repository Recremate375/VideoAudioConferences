using System;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using Prism.Events;
using System.ComponentModel;
using System.Security.RightsManagement;
using Emgu.CV.Structure;
using Emgu.CV;
using Emgu.CV.ImgHash;
using System.Threading;

namespace WPFClient.Services
{
	public class ShareScreenService : IDisposable
	{
		public delegate void ImageChangedEventHandler(object sender, Image<Bgr, byte> image);
		private BackgroundWorker shareScreenWorker;
		private int screenLeft = (int)SystemParameters.VirtualScreenLeft;
		private int screenTop = (int)SystemParameters.VirtualScreenTop;
		private int screenWidth = (int)SystemParameters.VirtualScreenWidth;
		private int screenHeight = (int)SystemParameters.VirtualScreenHeight;

		public ShareScreenService()
		{
			InitializedWorker();
		}

		private void InitializedWorker()
		{
			shareScreenWorker = new BackgroundWorker();
			shareScreenWorker.WorkerSupportsCancellation = true;
			shareScreenWorker.DoWork += OnShareScreenDoWork;
		}

		public event ImageChangedEventHandler ImageChanged;

		public bool IsRunning => shareScreenWorker?.IsBusy ?? false;

		private void RaiseImageChangedEvent(Image<Bgr, byte> image)
		{
			ImageChanged?.Invoke(this, image);
		}
		public void OnShareScreenDoWork(Object source, DoWorkEventArgs e)
		{
			if (shareScreenWorker.CancellationPending)
			{
				e.Cancel = true;
				return;
			}
			while (!shareScreenWorker.CancellationPending)
			{
				Bitmap bm_screen = new Bitmap(screenWidth, screenHeight);
				Graphics gs = Graphics.FromImage(bm_screen);

				gs.CopyFromScreen(screenLeft, screenTop, 0, 0, bm_screen.Size);
				RaiseImageChangedEvent(bm_screen.ToImage<Bgr, byte>());
				Thread.Sleep(16);
			}
		}

		public void RunServiceAsync()
		{
			shareScreenWorker?.RunWorkerAsync();
		}

		public void CancelServerAsync()
		{
			shareScreenWorker?.CancelAsync();
		}

		public void Dispose()
		{
			shareScreenWorker.DoWork -= OnShareScreenDoWork;
			shareScreenWorker.CancelAsync();
			shareScreenWorker?.Dispose();
		}
	}
}
