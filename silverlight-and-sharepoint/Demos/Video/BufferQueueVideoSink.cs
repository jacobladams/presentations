using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.IO;

namespace WebCamAviSaving.Recording
{
  public class BufferQueueVideoSink : VideoSink
  {
    public event EventHandler<RecordingProgressedEventArgs> RecordingProgressed;

    public BufferQueueVideoSink(RiffAviFileWriter aviFileWriter)
    {
      this.aviFileWriter = aviFileWriter;
    }
    protected override void OnCaptureStarted()
    {
      Reset();
    }
    private void Reset()
    {
      recordingStartTime = DateTime.Now;
      recordingProgressedEventArgs = new RecordingProgressedEventArgs();
    }
    protected override void OnCaptureStopped()
    {
      this.aviFileWriter.EndFile();
      Reset();
    }
    protected override void OnFormatChange(VideoFormat videoFormat)
    {
      this.aviFileWriter.StartFile((uint)videoFormat.Width, (uint)videoFormat.Height,
        (uint)videoFormat.FramesPerSecond);
    }
    protected override void OnSample(long sampleTime, long frameDuration, byte[] sampleData)
    {
      this.aviFileWriter.WriteFrame(sampleData);
      recordingProgressedEventArgs.FrameCount++;
      recordingProgressedEventArgs.Duration = DateTime.Now - recordingStartTime;
      FireRecordingProgressed();
    }
    void FireRecordingProgressed()
    {
      if (RecordingProgressed != null)
      {
        RecordingProgressed(this, recordingProgressedEventArgs);
      }
    }
    RiffAviFileWriter aviFileWriter;    
    RecordingProgressedEventArgs recordingProgressedEventArgs;
    DateTime recordingStartTime;
  }
}