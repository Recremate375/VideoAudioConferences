using DiplomClient.View.Pages;
using DiplomClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFClient.Services;
using WPFClient.Services.Interfaces;
using WPFClient.View.Pages;
using WPFClient.ViewModel;

namespace DiplomClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NavigationServiceToPages navigationService { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            navigationService = new NavigationServiceToPages(MainFraim);
            navigationService.NavigateTo<StartPageViewModel>();
        }
    }
}
