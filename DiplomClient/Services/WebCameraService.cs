using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace DiplomClient.Services
{
    public class WebCameraService : IDisposable
    {
        public delegate void ImageChangedEventHandler(object sender, Image<Bgr, byte> image);
        private readonly Logger logger= LogManager.GetCurrentClassLogger();
        private VideoCapture capture;

        private CascadeClassifier cascadeClassifier;
        private BackgroundWorker webCamWorker;

        public WebCameraService()
        {
            InitializeWorkers();
            InitializeClassifier();
        }

        private VideoCapture Capture
        {
            set => capture = value;
            get
            {
                try
                {
                    if (capture == null)
                    {
                        capture = new VideoCapture();
                        if (!capture.IsOpened)
                        {
                            capture.Start();
                        }
                        else
                        {
                            capture.Stop();
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.Error("Video capture failed! {ex}", ex);
                }
                return capture;
            }
        }

        public bool IsRunning => webCamWorker?.IsBusy ?? false;
        public void Dispose()
        {
            webCamWorker.DoWork -= OnWebCameraDoWork;
            webCamWorker.CancelAsync();
            Capture?.Dispose();
            Capture = null;
            webCamWorker?.Dispose();
        }

        private void InitializeClassifier()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var path = Path.GetDirectoryName(assembly.Location);

            //if (path != null)
            //{
            //    cascadeClassifier = new CascadeClassifier(Path.Combine(path, "haarcascade_frontalface_default.xml"));
            //}
            //else
            //{
            //    logger.Error("Couldn't find haarcascade xml file");
            //}
        }

        public event ImageChangedEventHandler ImageChanged;

        public void RunServiceAsync()
        {
            webCamWorker.RunWorkerAsync();
        }

        public void CancelServiceAsync()
        {
            webCamWorker?.CancelAsync();
        }

        private void RaiseImageChangedEvent(Image<Bgr, byte> image)
        {
            ImageChanged?.Invoke(this, image);
        }

        private void InitializeWorkers()
        {
            webCamWorker = new BackgroundWorker();
            webCamWorker.WorkerSupportsCancellation = true;
            webCamWorker.DoWork += OnWebCameraDoWork;
        }

        private void OnWebCameraDoWork(object sender, DoWorkEventArgs e)
        {
            if (webCamWorker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            while (!webCamWorker.CancellationPending)
            {
                var image = Capture.QueryFrame().ToImage<Bgr, byte>();

                //DetectFaces()
                RaiseImageChangedEvent(image);
            }
        }
    }
}
