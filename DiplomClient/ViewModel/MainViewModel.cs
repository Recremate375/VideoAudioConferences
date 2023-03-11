using System;
using System.Drawing;
using System.Windows.Input;
using DiplomClient.Services;
using Emgu.CV;
using Emgu.CV.Structure;
using NLog;

namespace DiplomClient.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private readonly Logger logger= LogManager.GetCurrentClassLogger();
        private string buttonContent = "Show Video";
        private Bitmap frame;
        private bool isStreaming;
        private WebCameraService webCameraService;

        public MainViewModel(Action methodAction)
        {
            InitializeCommands();
            CloseAction = methodAction;
        }

        public ICommand ToggleCameraServiceCommand { get; set; }
        public ICommand ToggleCloseAppCommand { get; set; }

        public Action CloseAction { get; set; }

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

        private void InitializeWebCamService()
        {
            webCameraService = new WebCameraService();
            webCameraService.ImageChanged += OnCameraImageChanged;
        }

        private void OnCameraImageChanged(object sender, Image<Bgr, byte> image)
        {
            Frame = image.AsBitmap(); // ???????
        }

        private void InitializeCommands()
        {
            ToggleCameraServiceCommand = new RelayCommand(ToggleCameraServiceExecute);
            ToggleCloseAppCommand = new RelayCommand(ToggleCloseApp);

        }

        private void ClearFrame()
        {
            if (Frame != null)
            {
                Frame = null;
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
                webCameraService.Dispose();
                webCameraService = null;
                logger.Info("Video streaming stopped!");
            }
        }
    }
}
