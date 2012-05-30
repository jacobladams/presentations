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
  public class RiffChunk
  {
    public RiffChunk(Stream stream) : 
      this(stream, RiffAviFourCCCode.Riff)
	  {            
	  }
    RiffChunk(Stream stream, RiffAviFourCCCode code) : 
      this(stream, code, null)
    {
    }
    RiffChunk(Stream stream, RiffAviFourCCCode code, RiffChunk parent)
    {
      this.stream = stream;

      WriteBits(RiffAviFourCCCodes.GetCode(code));      

      bookmark = new StreamBookmark(this.stream);
      bookmark.CaptureAndWrite(0);
      size = 0;

      this.parentChunk = parent;

      if (this.parentChunk != null)
      {
        this.parentChunk.size += 8;
      }
    }
    public StreamBookmark Bookmark
    {
      get
      {
        return (this.bookmark);
      }
    }
    public RiffChunk AddChild(RiffAviFourCCCode code)
    {
      return (new RiffChunk(this.stream, code, this));
    }
    public void Close()
    {
      bookmark.ReplaceValue(size);

      if (parentChunk != null)
      {
        parentChunk.size += this.size;
      }
    }
    public void WriteFourCC(RiffAviFourCCCode code)
    {
      WriteBits(RiffAviFourCCCodes.GetCode(code));
    }
    public void WriteWORD(UInt16 word)
    {
      WriteBits(BitConverter.GetBytes(word));
    }
    public void WriteDWORD(Int32 value)
    {
      WriteDWORD((UInt32)value);
    }
    public void WriteDWORD(UInt32 value)
    {
      WriteBits(BitConverter.GetBytes(value));
    }
    public StreamBookmark WriteAndBookmarkDWORD(UInt32 value)
    {
      StreamBookmark bookmark = new StreamBookmark(this.stream);
      bookmark.CaptureAndWrite(value);
      this.size += 4;
      return (bookmark);
    }
    public void WriteBits(byte[] bits)
    {
      WriteBits(bits, 0, bits.Length);
    }
    public void WriteBits(byte[] bits, int index, int length)
    {
      stream.Write(bits, index, length);
      this.size += (uint)length;
    }
    public void WriteFrameAsync(byte[] argb32bppFrame, Action callback)
    {
      WriteFourCC(RiffAviFourCCCode.DSig);
      WriteDWORD(argb32bppFrame.Length);
      this.size += (uint)(argb32bppFrame.Length);

      stream.BeginWrite(argb32bppFrame, 0, argb32bppFrame.Length, iar =>
        {
          // TODO - throws!
          stream.EndWrite(iar);

          if (callback != null)
          {
            callback();
          }
        }, null);
    }
    RiffChunk parentChunk;
    StreamBookmark bookmark;
    uint size;
    Stream stream;
  }
}