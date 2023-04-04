using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DiplomClient.Services;
using DiplomClient.View.Pages;
using Emgu.CV;
using Emgu.CV.Structure;
using NLog;
using Prism.Commands;

namespace DiplomClient.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private Page _currentPage;

        public MainViewModel()
        {
            _currentPage = new CallPage();
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
