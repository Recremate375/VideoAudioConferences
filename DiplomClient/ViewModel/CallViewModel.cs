using DiplomClient.Services;
using Emgu.CV.Structure;
using Emgu.CV;
using NLog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Emgu.CV.Cuda;
using System.IO;
using WPFClient.Services;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows;
using WPFClient.Services.Interfaces;

namespace DiplomClient.ViewModel
{
    internal class CallViewModel : BaseViewModel
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private string buttonContent = "Show Video";
        private Bitmap frame;
        private Bitmap userFrame;
        private Bitmap screenFrame;
        private Bitmap shareScreenButtonFrame;
        private Bitmap webCameraButtonFrame;
        private Bitmap microfoneButtonFrame;
        private bool isStreaming;
        private bool isScreenStreaming;
        private WebCameraService webCameraService;
        private ShareScreenService shareScreenService;
        private CallService callService;
        private ObservableCollection<Image> images;
        private List<Bitmap> framesOfUsers;
        private string userName;
        public CallViewModel()
        {
            InitializeCommands();
            LoadImage("img\\animeArt.jpg", out frame);
            LoadImage("img\\kaneki.jpg", out  userFrame);
            LoadImage("img\\Monitor.png", out shareScreenButtonFrame);
            LoadImage("img\\openCameraButton.png", out webCameraButtonFrame);
            LoadImage("img\\Microphone.png", out microfoneButtonFrame);
            userName = "Kaneki";
            //CloseAction = methodAction;
        }

        public string UserName {
            get => userName;
            set => SetField(ref userName, value);
        }

        public Bitmap ShareScreenButtonFrame
        {
            get => shareScreenButtonFrame;
            set => SetField(ref shareScreenButtonFrame, value);
        }

        public Bitmap WebCameraButtonFrame
        {
            get => webCameraButtonFrame;
            set => SetField(ref webCameraButtonFrame, value);
        }

        public Bitmap MicrofoneButtonFrame
        {
            get => microfoneButtonFrame;
            set => SetField(ref microfoneButtonFrame, value);
        }


        private void LoadImage(string path, out Bitmap frame)
        {
            using (FileStream stream = File.OpenRead(path))
            {
                Bitmap bitmap = new Bitmap(stream);
				MemoryStream memoryStream = new MemoryStream();
				bitmap.Save(memoryStream, ImageFormat.Png);
				memoryStream.Seek(0, SeekOrigin.Begin);
                frame = new Bitmap(memoryStream);
			}
		}

        public ICommand ToggleScreenServiceCommand { get; set; }
        public ICommand ToggleCameraServiceCommand { get; set; }
        public ICommand ToggleCloseAppCommand { get; set; }
        public ICommand CallCommand { get; set; }

        public Action CloseAction { get; set; }

        public ObservableCollection<Image> Images
        {
            get => images;
            set => SetField(ref images, value);
        }

        public string ButtonContent
        {
            get => buttonContent;
            set => SetField(ref buttonContent, value);
        }

        public bool IsStreaming
        {
            get => isStreaming;
            set => SetField(ref isStreaming, value);
        }

        public Bitmap Frame
        {
            get => frame;
            set => SetField(ref frame, value);
        }
        public Bitmap ScreenFrame
        {
            get => screenFrame;
            set => SetField(ref screenFrame, value);
        }

        public Bitmap UserFrame
        {
            get => userFrame;
            set => SetField(ref  userFrame, value);
        }

        private void InitializeWebCamService()
        {
            webCameraService = new WebCameraService();
            webCameraService.ImageChanged += OnCameraImageChanged;
        }

        private void InitializeShareScreenService()
        {
            shareScreenService = new ShareScreenService();
            shareScreenService.ImageChanged += OnScreenImageChanged;
        }

        private void OnCameraImageChanged(object sender, Image<Bgr, byte> image)
        {
            Frame = image.AsBitmap(); 
        }

        private void OnScreenImageChanged(object sender, Image<Bgr, byte> image)
        {
            ScreenFrame = image.AsBitmap();
        }

        private void InitializeCommands()
        {
            ToggleCameraServiceCommand = new RelayCommand(ToggleCameraServiceExecute);
            ToggleCloseAppCommand = new RelayCommand(ToggleCloseApp);
            ToggleScreenServiceCommand = new RelayCommand(ToggleShareScreenServiceExecute);
            CallCommand = new RelayCommand(SendVideoStreaming);
        }

        private void ClearFrame()
        {
            if (Frame != null)
            {
                Frame = null;
            }
        }

        private void ClearScreenFrame()
        {
            if(ScreenFrame != null)
            {
                ScreenFrame = null;
            }
        }

        private void ToggleCloseApp()
        {
            CloseAction?.Invoke();
            webCameraService?.Dispose();
        }

        private void ToggleCameraServiceExecute()
        {
            if (webCameraService == null)
            {
                InitializeWebCamService();
            }

            if (!webCameraService.IsRunning)
            {
                IsStreaming = true;
                ButtonContent = "Stop Video";
                webCameraService.RunServiceAsync();
                logger.Info("Video streaming is started!");
            }
            else
            {
                IsStreaming = false;
                ButtonContent = "Show Video";
                webCameraService.CancelServiceAsync();
                ClearFrame();
				LoadImage("img\\animeArt.jpg", out frame);
				webCameraService.Dispose();
                webCameraService = null;
                logger.Info("Video streaming stopped!");
            }
        }
        private void SendVideoStreaming()
        {

        }

		private void ToggleShareScreenServiceExecute()
        {
            if(shareScreenService == null)
            {
                InitializeShareScreenService();
            }

            if (!shareScreenService.IsRunning)
            {
                isScreenStreaming = true;
                shareScreenService.RunServiceAsync();
            }
            else
            {
                isScreenStreaming = false;
                shareScreenService.CancelServerAsync();
				ClearScreenFrame();
                shareScreenService.Dispose();
                shareScreenService = null;
                ScreenFrame = null;
            }
        }

    }
}
