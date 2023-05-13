using DiplomClient.Model;
using DiplomClient.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFClient.Model;

namespace WPFClient.ViewModel
{
	public class CreateChannelViewModel :BaseViewModel
	{
		private Bitmap frame;
		private string channelName;
		private byte[] channelImage;
		private User currentUser;
		private Channel newChannel = new();
		public CreateChannelViewModel()
		{
			InitializeCommand();
			OpenFile("img\\AddButton.png");
		}
		public string ChannelName
		{
			get { return channelName; }
			set { SetField(ref channelName, value); }
		}
		public Bitmap Frame
		{
			get { return frame; }
			set { SetField(ref frame, value); }
		}
		public ICommand OpenFileDialogCommand { get; set; }
		public ICommand CreateChannel { get; set; }

		private void OpenFileDialog()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Image Files (*.jpg;*.png;*.gif;*.bmp)|*.jpg;*.png;*.gif;*.bmp|All Files (*.*)|*.*";
			if (openFileDialog.ShowDialog() == true)
			{
				string selectedFileName = openFileDialog.FileName;
				OpenFile(selectedFileName);
			}
		}

		private void OpenFile(string filePath)
		{
			using (FileStream stream = File.OpenRead(filePath))
			{
				Bitmap bitmap = new Bitmap(stream);
				MemoryStream memoryStream = new MemoryStream();
				bitmap.Save(memoryStream, ImageFormat.Bmp);
				memoryStream.Seek(0, SeekOrigin.Begin);
				Frame = new Bitmap(memoryStream);
				channelImage = memoryStream.ToArray();
			}
		}

		private void CreateNewChannel()
		{
			if (ChannelName == null)
			{
				MessageBox.Show("Enter a channel name");
			}
			else
			{
				newChannel.Name = ChannelName;
				newChannel.ChannelImage = channelImage;
				newChannel.Users.Add(currentUser);

			}
		}

		private void InitializeCommand()
		{
			OpenFileDialogCommand = new RelayCommand(OpenFileDialog);
			CreateChannel = new RelayCommand(CreateNewChannel);
		}

	}
}
