using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Services.Interfaces
{
	public interface IUserData
	{
		void SetLoginData(string login);
		string GetLoginData();
	}
}
