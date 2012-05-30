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
using System.IO;

namespace WebCamAviSaving.Recording.RiffAviFileRecording
{
  public class StreamBookmark
  {
    public StreamBookmark(Stream stream)
    {
      this.stream = stream;
    }
    public void CaptureAndWrite(uint value)
    {
      capturePosition = stream.Position;
      stream.Write(BitConverter.GetBytes(value), 0, 4);
    }
    public void ReplaceValue(uint value)
    {
      long currentPosition = stream.Position;
      stream.Seek(capturePosition, SeekOrigin.Begin);
      stream.Write(BitConverter.GetBytes(value), 0, 4);
      stream.Seek(currentPosition, SeekOrigin.Begin);
    }
    Stream stream;
    long capturePosition;
  }
}
