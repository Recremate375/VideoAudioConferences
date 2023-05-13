using DiplomClient.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WPFClient.Model;
using WPFClient.Services.Interfaces;
using WPFClient.View;

namespace DiplomClient.ViewModel
{
    public class StartPageViewModel : BaseViewModel
    {
        private IUserData _userData;
        private const string url = "https://localhost:7156/";
        private User user;
        private WPFClient.Model.Channel choosedChannel; 
        private List<WPFClient.Model.Channel> channels;
        private Bitmap userImage;
        public Bitmap UserImage
        {
            get { return userImage; }
            set { SetField(ref userImage, value); }
        }
        public List<WPFClient.Model.Channel> Channels
        { 
            get { return channels; }
            set { SetField(ref channels, value); }
        }
        public User User
        {
            get { return user; }
            set { SetField(ref user, value); }
        }
        public WPFClient.Model.Channel ChoosedChannel
        {
            get { return choosedChannel; }
            set { SetField(ref choosedChannel, value); }
        }

        public StartPageViewModel(IUserData userData)
        {
            _userData = userData;
            //GetUserInformation();
            InitializeCommand();
            LoadImage("img\\animeArt.jpg");
            user = new User()
            {
                Id = 1,
                Name = "Recremate",
                Email = "111@mail.ru",
                
            };
        }

		private void LoadImage(string path)
		{
			using (FileStream stream = File.OpenRead(path))
			{
				Bitmap bitmap = new Bitmap(stream);
				MemoryStream memoryStream = new MemoryStream();
				bitmap.Save(memoryStream, ImageFormat.Bmp);
				memoryStream.Seek(0, SeekOrigin.Begin);
				userImage = new Bitmap(memoryStream);
			}
		}


		public ICommand SendMessage { get; set; }
        public ICommand StartAudioCall { get; set; }
        public ICommand StartVideoCall { get; set; }
        public ICommand ChangeChannel { get; set; }
        public ICommand CreateNewChannel { get; set; }

        private void InitializeCommand()
        {
            SendMessage = new RelayCommand(sendMessage);
            StartAudioCall = new RelayCommand(startAudioCall);
            StartVideoCall = new RelayCommand(startVideoCall);
            ChangeChannel = new RelayCommand(changeChannel);
            CreateNewChannel = new RelayCommand(createNewChannel);
        }

        private void GetInformationAboutChannel()
        {

        }

        private void GetUserInformation()
        {


			//channels = user.Channels;
		}

		private void startAudioCall()
		{

		}

		private void startVideoCall()
        {

        }

        private void sendMessage()
        {

        }

        private void changeChannel()
        {

        }
        private void createNewChannel()
        {
            CreateChannelWindow createChannelWindow = new CreateChannelWindow();
            createChannelWindow.Show();
        }
    }
}
