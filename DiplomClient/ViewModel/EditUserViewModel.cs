using DiplomClient.Model;
using DiplomClient.ViewModel;
using Microsoft.Win32;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPFClient.ViewModel
{
	public class EditUserViewModel : BaseViewModel
	{
		private readonly string serverUrl = "https://localhost:7156/api/Users/";
		private readonly HttpClient httpClient = new HttpClient();
		private User currentUser = new User();
		private Bitmap userImage;
		private bool userHaveImage = false;
		public Bitmap UserImage
		{
			get { return userImage; }
			set { SetField(ref userImage, value); }
		}
		public User CurrentUser
		{
			get { return currentUser; }
			set { SetField(ref currentUser, value); }
		}

		public EditUserViewModel()
		{
			InitializeCommand();
			if (currentUser.UserImage != null)
			{
				userHaveImage = true;
			}
			OpenFile("img\\AddButton.png");
		}

		public ICommand choosedImageCommand { get; set; }
		public ICommand saveChanges { get; set; }
		public ICommand goBackCommand { get; set; }
		private void InitializeCommand()
		{
			choosedImageCommand = new RelayCommand(OpenFileDialog);
			saveChanges = new RelayCommand(SaveChanges);
			goBackCommand = new RelayCommand(GoBack);
		}

		private void OpenFileDialog()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Image Files (*.jpg;*.png;*.gif;*.bmp)|*.jpg;*.png;*.gif;*.bmp|All Files (*.*)|*.*";
			if (openFileDialog.ShowDialog() == true)
			{
				string selectedFileName = openFileDialog.FileName;
				userHaveImage = false;
				OpenFile(selectedFileName);
			}
		}

		private void OpenFile(string filePath)
		{
			if (!userHaveImage)
			{
				using (FileStream stream = File.OpenRead(filePath))
				{
					Bitmap bitmap = new(stream);
					MemoryStream memoryStream = new MemoryStream();
					bitmap.Save(memoryStream, ImageFormat.Png);
					memoryStream.Seek(0, SeekOrigin.Begin);
					UserImage = new Bitmap(memoryStream);
					userHaveImage = true;
					currentUser.UserImage = memoryStream.ToArray();
				}
			}
		}
		private async void SaveChanges()
		{
			MessageBox.Show("The username is too long, please shorten it to 10 characters.");
			//string json = Newtonsoft.Json.JsonConvert.SerializeObject(currentUser);

			//var content = new StringContent(json, Encoding.UTF8, "application/json");
			//var response = await httpClient.PutAsync(serverUrl + $"{currentUser.Id}", content);

			//if (response.IsSuccessStatusCode)
			//{
			//	MessageBox.Show("Data saved successfully");
			//}
			//else
			//{
			//	var errorMessage = await response.Content.ReadAsStringAsync();
			//	MessageBox.Show(errorMessage, "Error!");
			//}

		}

		private async void asyncSaveChanges()
		{

		}

		private void GoBack()
		{
			NavigationService.NavigateTo<StartPageViewModel>();
		}
	}
}
