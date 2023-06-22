using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFClient.Model;
using WPFClient.Services.Interfaces;

namespace DiplomClient.ViewModel
{
	public class LoginViewModel : BaseViewModel
	{
		public ICommand SignUpCommand { get; set; }

		private string login;
		private string password;
		private readonly IUserData _userData;

		public LoginViewModel(IUserData userData)
		{
			InitializeCommand();
			_userData = userData;
		}

		public string Login {
			get => login;
			set => SetField(ref login, value);
		}

		public string Password
		{
			get => password;
			set => SetField(ref password, value);
		}

		private void InitializeCommand()
		{
			SignUpCommand = new RelayCommand(SignUp);
		}

		private async void SignUp()
		{
			string url = "https://localhost:7016/api/Account/login";
			if (password != null)
			{
				string? stringPassword = password.ToString();
				var token = await GetJwtToken(url, login, stringPassword);
				_userData.SetLoginData(login);
				//MessageBox.Show(_userData.GetLoginData());
				MainWindow windows = new MainWindow();
				Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive)?.Close();
				windows.Show();


			}
			else
			{
				MessageBox.Show("Enter the password!");
			}
		}


		private async Task<string> GetJwtToken(string url, string login, string thisPassword)
		{
			var credentials = new
			{
				email = login,
				password = thisPassword
			};

			using(var client = new HttpClient())
			{
				var content = new StringContent(JsonConvert.SerializeObject(credentials), Encoding.UTF8, "application/json");
				var response = await client.PostAsync(url, content);

				if (response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadAsStringAsync();
					try
					{
						var token = JsonConvert.DeserializeObject<StringToken>(result);
						return token.Token;
					}
					catch(Exception ex)
					{
						throw new Exception(ex.Message);
					}
				}
				else
				{
					MessageBox.Show("Request failed with status code" + response.StatusCode);
					throw new Exception("Request failed with status code " + response.StatusCode);
				}
			}
		}
	}
}
