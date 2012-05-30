using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO;
using WebCamAviSaving.Recording;

namespace Video
{
	public partial class MainPage : UserControl
	{
		private CaptureSource _captureSource = new CaptureSource();
		private BufferQueueVideoSink _videoSink;
		private MemoryStream _videoStream;
		

		public MainPage()
		{
			InitializeComponent();
		}

		private void StartCapture_Click(object sender, RoutedEventArgs e)
		{
			_captureSource.VideoCaptureDevice = CaptureDeviceConfiguration.GetDefaultVideoCaptureDevice();
			_captureSource.AudioCaptureDevice = CaptureDeviceConfiguration.GetDefaultAudioCaptureDevice();

			VideoBrush videoBrush = new VideoBrush();
			videoBrush.Stretch = Stretch.Uniform;
			videoBrush.SetSource(_captureSource);

			_videoStream = new MemoryStream();
			RiffAviFileWriter aviFileWriter = new RiffAviFileWriter(_videoStream);
			_videoSink = new BufferQueueVideoSink(aviFileWriter)
			{
				CaptureSource = _captureSource
			};


			if (CaptureDeviceConfiguration.AllowedDeviceAccess || CaptureDeviceConfiguration.RequestDeviceAccess())
				_captureSource.Start();
			Video.Fill = videoBrush;
		}

		private void StopCapture_Click(object sender, RoutedEventArgs e)
		{
			_captureSource.Stop();
			_videoSink.CaptureSource = null;
			//todo: write file
			_videoStream.Close();

		}

		void StartRecording(Stream stream)
		{
			_captureSource.Stop();

			RiffAviFileWriter aviFileWriter = new RiffAviFileWriter(stream);

			_videoSink = new BufferQueueVideoSink(aviFileWriter)
			{
				CaptureSource = _captureSource
			};

			//videoSink.RecordingProgressed += (s, e) =>
			//{
			//    Dispatcher.BeginInvoke(() =>
			//    {
			//        this.RecordingProgress = e;
			//    });
			//};

			_captureSource.Start();
		}
		
	}
}
