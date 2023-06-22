using DiplomClient.Model;
using DiplomClient.View.Pages;
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
using WPFClient.Services;
using WPFClient.Services.Interfaces;
using WPFClient.View;
using WPFClient.View.Pages;
using WPFClient.ViewModel;

namespace DiplomClient.ViewModel
{
    public class StartPageViewModel : BaseViewModel
    {
        private IUserData _userData;
        private const string url = "https://localhost:7156/";
        private User user;
        private WPFClient.Model.Channel channel;
        private WPFClient.Model.Channel choosedChannel; 
        private ObservableCollection<WPFClient.Model.Channel> channels;
        private Bitmap userImage;
        private string userName;
        private Bitmap settingFrame;
        private Bitmap audioCallFrame;
        private Bitmap videoCallFrame;
        public Bitmap AudioCallFrame
        {
            get { return audioCallFrame; }
            set { SetField(ref audioCallFrame, value); }
        }
        public Bitmap VideoCallFrame
        {
            get { return videoCallFrame; }
            set { SetField(ref videoCallFrame, value); }
        }
        public Bitmap UserImage
        {
            get { return userImage; }
            set { SetField(ref userImage, value); }
        }
        public Bitmap SettingFrame
        {
            get { return settingFrame; }
            set { SetField(ref  settingFrame, value); }
        }
        public ObservableCollection<WPFClient.Model.Channel> Channels
        { 
            get { return channels; }
            set { SetField(ref channels, value); }
        }
        public User User
        {
            get { return user; }
            set { SetField(ref user, value); }
        }

        public string UserName
        {
            get { return userName; }
            set { SetField(ref userName, value); }
        }

        public WPFClient.Model.Channel ChoosedChannel
        {
            get { return choosedChannel; }
            set { SetField(ref choosedChannel, value); }
        }

        private byte[] channelImage;
        private Bitmap ChannelImage;

        public StartPageViewModel()
        {
            //_userData = userData;
            InitializeCommand();
            LoadImage("img\\animeArt.jpg", out userImage);
            LoadImage("img\\settings.png", out settingFrame);
            LoadImage("img\\callButton.png", out audioCallFrame);
            LoadImage("img\\videoCallButton.png", out videoCallFrame);

            user = new User()
            {
                Id = 1,
                Name = "Recremate",
                Email = "user@user.com",
                userImage = userImage
            };

			//channels.Add(channel);
			GetUserInformation();
		}

		private void LoadImage(string path, out Bitmap thisframe)
		{
			using (FileStream stream = File.OpenRead(path))
			{
				Bitmap bitmap = new Bitmap(stream);
				MemoryStream memoryStream = new MemoryStream();
				bitmap.Save(memoryStream, ImageFormat.Png);
				memoryStream.Seek(0, SeekOrigin.Begin);
				thisframe = new Bitmap(memoryStream);
				channelImage = memoryStream.ToArray();
			}
		}

		public ICommand SendMessage { get; set; }
        public ICommand StartAudioCall { get; set; }
        public ICommand StartVideoCall { get; set; }
        public ICommand ChangeChannel { get; set; }
        public ICommand CreateNewChannel { get; set; }
        public ICommand EditUser { get; set; }
        private void InitializeCommand()
        {
            SendMessage = new RelayCommand(sendMessage);
            StartAudioCall = new RelayCommand(startAudioCall);
            StartVideoCall = new RelayCommand(startVideoCall);
            ChangeChannel = new RelayCommand(changeChannel);
            CreateNewChannel = new RelayCommand(createNewChannel);
            EditUser = new RelayCommand(editUser);
        }

        private void GetInformationAboutChannel()
        {

        }

        private void GetChannelsForUser()
        {
			
		}

        private void GetUserInformation()
        {


            if (user.Name == null)
            {
                UserName = user.Email;
            }
            else
            {
                UserName = user.Name;
            }

			//channels = user.Channels;
		}

        private void editUser()
        {
            NavigationService.NavigateTo<EditUserViewModel>();
        }

		private void startAudioCall()
		{
			NavigationService.NavigateTo<CallViewModel>();
		}

		private void startVideoCall()
        {

        }

        private void sendMessage()
        {
			LoadImage("img\\9Q93_zMIeeo.jpg", out ChannelImage);

            Message message = new Message
            {
                message = "Hello world!",
                messageDate = DateTime.Now,
                user = user,
                Id = 1
            };

            channel = new WPFClient.Model.Channel()
			{
				Name = "Channel1",
				Id = 1,
				Users = new List<User> { user },
                channelImage = ChannelImage,
                MessageHistory = new List<Message> { message }
			};
            Channels = new ObservableCollection<WPFClient.Model.Channel> { channel };
            //ChoosedChannel = channel;
			//Channels.Add(channel);
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
