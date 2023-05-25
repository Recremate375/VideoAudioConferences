using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DiplomClient.Model;
using DiplomClient.Services;
using DiplomClient.View.Pages;
using WPFClient.Services;
using WPFClient.Services.Interfaces;
using WPFClient.View.Pages;
using WPFClient.ViewModel;

namespace DiplomClient.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private Page _currentPage;

        private ObservableCollection<User> users;

        public MainViewModel(IUserData userData)
        {
            //_currentPage = new EditUserPage();
            //NavigationServiceToPages.Instance.RegisterPage("StartPage", typeof(StartPage));
            //NavigationServiceToPages.Instance.RegisterPage("CallPage", typeof(CallPage));
            //NavigationServiceToPages.Instance.RegisterPage("EditUserPage", typeof(EditUserPage));
            //_currentPage = new StartPage(userData); 
        }
        public Page CurrentPage
        {
            get { return _currentPage; }
            set
            {
                if(_currentPage != value )
                {
                    _currentPage = value;
                    OnPropertyChanged(nameof(CurrentPage));
                }
            }
        }

        private ICommand _openCallPageCommand;
        public ICommand OpenCallPageCommand
        {
            get
            {
                if (_openCallPageCommand == null)
                {
                    _openCallPageCommand = new RelayCommandForPages(
                        param => this.OpenCallPage(),
                        param => true
                        );
                }
                return _openCallPageCommand;
            }
        }
        private void OpenCallPage()
        {
            CurrentPage = new CallPage();
        }

    }
}
