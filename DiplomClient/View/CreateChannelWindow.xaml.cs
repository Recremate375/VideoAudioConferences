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
using System.Windows.Shapes;
using WPFClient.ViewModel;

namespace WPFClient.View
{
	/// <summary>
	/// Логика взаимодействия для CreateChannelWindow.xaml
	/// </summary>
	public partial class CreateChannelWindow : Window
	{
		public CreateChannelWindow()
		{
			InitializeComponent();
			var ViewModel = new CreateChannelViewModel();
			DataContext = ViewModel;
		}
	}
}
