using DiplomClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClient.Model;
using WPFClient.Services.Interfaces;

namespace WPFClient.Services
{
	public class UserData : IUserData
	{
		private static UserData _instance;
		public string? UserEmail;
		public User? currentUser;

		private UserData() { }

		public static UserData Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new UserData();
				}
				return _instance;
			}
		}

		public void SetLoginData(string login)
		{
			UserEmail = login;
		}
		public void SetUserData(User user)
		{
			currentUser = user;
		}
		public string GetLoginData()
		{
			return UserEmail;
		}
		public User GetUserData()
		{
			return currentUser;
		}
	}
}
