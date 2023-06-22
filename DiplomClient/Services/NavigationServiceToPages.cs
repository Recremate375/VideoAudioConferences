using DiplomClient;
using DiplomClient.View;
using DiplomClient.View.Pages;
using DiplomClient.ViewModel;
using NLog.LayoutRenderers;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPFClient.View.Pages;
using WPFClient.ViewModel;

namespace WPFClient.Services
{
	public class NavigationServiceToPages
	{
		private readonly Dictionary<Type, Type> pageViewModels;
		private readonly Frame _frame;
		public NavigationServiceToPages(Frame frame)
		{
			_frame = frame;
			pageViewModels = new Dictionary<Type, Type>();
			pageViewModels.Add(typeof(StartPageViewModel), typeof(StartPage));
			pageViewModels.Add(typeof(CallViewModel), typeof(CallPage));
			pageViewModels.Add(typeof(EditUserViewModel), typeof(EditUserPage));
		}
		public void NavigateTo<TViewModel>(object parameter = null) where TViewModel : BaseViewModel
		{
			Type viewModelType = typeof(TViewModel);
			if(pageViewModels.TryGetValue(viewModelType, out Type viewType))
			{
				var view = (Page)Activator.CreateInstance(viewType);
				var viewModel = (BaseViewModel)Activator.CreateInstance(viewModelType);
				viewModel.NavigationService = this;
				viewModel.Initialize(parameter);
				view.DataContext = viewModel;
				_frame.Navigate(view);
			}
		}
	}
}
