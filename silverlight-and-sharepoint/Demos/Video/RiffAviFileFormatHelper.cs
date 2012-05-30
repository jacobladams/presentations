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
using System.Windows.Threading;
using System.Collections.Generic;

namespace SilverlightApplication24
{
  public class RiffAviFileWriter
  {
    class MarkPoint
    {
      public long FileOffset;
      public long ByteCount;
    }
    public RiffAviFileWriter(IsoStoreFileHeader header, Stream stream)
    {
      this.header = header;
      this.stream = stream;
      markPoints = new Stack<MarkPoint>();
    }
    public void Close()
    {
      ClearMarkPoint(); // For the list size
      ClearMarkPoint(); // For the file size
      stream.Flush();
      stream.Close();
    }
    void StartChunk(byte[] chunkType, byte[] chunkId = null)
    {
      WriteBits(chunkType);
      AddMarkPoint();

      if (chunkId != null)
      {
        WriteBits(chunkId);
      }
    }
    void EndChunk()
    {
      ClearMarkPoint();
    }
    void WriteAvihHeaderStructure()
    {
      UInt32 microSecondsPerFrame =
        (UInt32)((1.0 / header.FramesPerSecond) * 1.0E6);

      WriteDWORD(microSecondsPerFrame);             // Microseconds per frame
      WriteDWORD(0);                                // Max Bytes Per Second
      WriteDWORD(0);                                // Padding granularity
      WriteDWORD(0);                                // Flags
      WriteDWORD(header.FrameCount);                // Total frames
      WriteDWORD(0);                                // Initial frames
      WriteDWORD(1);                                // Streams
      WriteDWORD(header.Width * header.Height * 3); // Suggested buffer size
      WriteDWORD(header.Width);                     // Width
      WriteDWORD(header.Height);                    // Height
      WriteDWORD(0);                                // Reserved [0]
      WriteDWORD(0);                                // Reserved [1]
      WriteDWORD(0);                                // Reserved [2]
      WriteDWORD(0);                                // Reserved [3]
    }
    void WriteStrhHeaderStructure()
    {
      WriteBits(fourCCVids);                          // 'vids'
      WriteBits(fourCCDib);                           // 'DIB '
      WriteDWORD(0);                                  // flags
      WriteWORD(0);                                   // priority
      WriteWORD(0);                                   // language      
      WriteDWORD(0);                                  // initial frames
      WriteDWORD(1);                                  // scale
      WriteDWORD(header.FramesPerSecond);             // rate
      WriteDWORD(0);                                  // start
      WriteDWORD(header.FrameCount);                  // length
      WriteDWORD(header.Width * header.Height * 3);   // suggested buffer size
      WriteDWORD(0);                                  // quality
      WriteDWORD(0);                                  // sample size
      WriteDWORD(0);                                  // left
      WriteDWORD(0);                                  // top
      WriteDWORD((ushort)header.Width);                       // right
      WriteDWORD((ushort)header.Height);                      // bottom
    }
    void WriteStrfHeaderStructure()
    {
      WriteDWORD(40);                                 // structure size - TOTAL structure size              
      WriteDWORD(header.Width);                       // width
      WriteDWORD(header.Height);                      // height
      WriteWORD(1);                                   // planes
      WriteWORD(24);                                  // bits per pixel
      WriteDWORD(0);                                  // compression ( BI_RGB == 0 )
      WriteDWORD(header.Width * header.Height * 3);   // size image - TODO
      WriteDWORD(0);                                  // X pels per meter
      WriteDWORD(0);                                  // Y pels per meter
      WriteDWORD(0);                                  // clr used
      WriteDWORD(0);                                  // clr important 
      //NB: No colour table.
    }
    public void WriteRiffFileHeader()
    {
      // This comes from reading
      // http://www.webkinesia.com/games/videoformats.php
      // http://msdn.microsoft.com/en-us/library/dd318229(VS.85).aspx 
      // http://rsbweb.nih.gov/ij/plugins/download/AVI_Writer.java 
      // http://www.opennet.ru/docs/formats/avi.txt
      // and that's all I know about this file format.

      // 'RIFF'
      StartChunk(fourCCRiff, fourCCAvi);
        
        // 'LIST' 'hdrl'
        StartChunk(fourCCList, fourCCHdrl);
          
          // 'avih' structure
          StartChunk(fourCCAvih);
            WriteAvihHeaderStructure();
          EndChunk();

          // 'strl' list
          StartChunk(fourCCList, fourCCStrl);

            // 'strh'
            StartChunk(fourCCStrh);
              WriteStrhHeaderStructure();
            EndChunk();

            // 'strf'
            StartChunk(fourCCStrf);
              WriteStrfHeaderStructure();
            EndChunk();

          EndChunk();

        EndChunk();

        // 'LIST' movi'
        StartChunk(fourCCList, fourCCMovi);
    }
    void AddMarkPoint()
    {
      MarkPoint mp = new MarkPoint();
      markPoints.Push(mp);

      mp.FileOffset = stream.Position;
      stream.Write(new byte[] { 0, 0, 0, 0 }, 0, 4);
    }
    void ClearMarkPoint()
    {
      MarkPoint mp = markPoints.Pop();
       
      stream.Seek(mp.FileOffset, SeekOrigin.Begin);
      WriteDWORD((UInt32)mp.ByteCount);
      stream.Seek(0, SeekOrigin.End);

      if (markPoints.Count > 0)
      {
        markPoints.Peek().ByteCount += mp.ByteCount;
      }
    }
    public void WriteFrame(byte[] frameBits)
    {
      WriteBits(fourCCDSig);         // '00db'
      AddMarkPoint();                // data length

      // We've got 32bppARGB which we need to write out as 24bpp RGB ( I think )
      // so let's try that.
      for (int i = 0; i < frameBits.Length; i += 4)
      {
        WriteBits(frameBits, i, 3);
      }
      ClearMarkPoint();               // data length
    }
    void WriteWORD(UInt16 word)
    {
      WriteBits(BitConverter.GetBytes(word));
    }
    void WriteDWORD(Int32 value)
    {
      WriteDWORD((UInt32)value);
    }
    void WriteDWORD(UInt32 value)
    {
      WriteBits(BitConverter.GetBytes(value));
    }
    void WriteBits(byte[] bits)
    {
      WriteBits(bits, 0, bits.Length);
    }
    void WriteBits(byte[] bits, int index, int length)
    {
      stream.Write(bits, index, length);

      if (markPoints.Count > 0)
      {
        markPoints.Peek().ByteCount += length;
      }
    }
    IsoStoreFileHeader header;
    Stream stream;
    Stack<MarkPoint> markPoints;

    static byte[] fourCCRiff = new byte[] { 0x52, 0x49, 0x46, 0x46 };
    static byte[] fourCCAvi = new byte[]  { 0x41, 0x56, 0x49, 0x20 };
    static byte[] fourCCList = new byte[] { 0x4C, 0x49, 0x53, 0x54 };
    static byte[] fourCCHdrl = new byte[] { 0x68, 0x64, 0x72, 0x6C };
    static byte[] fourCCAvih = new byte[] { 0x61, 0x76, 0x69, 0x68 };
    static byte[] fourCCStrl = new byte[] { 0x73, 0x74, 0x72, 0x6C };
    static byte[] fourCCStrh = new byte[] { 0x73, 0x74, 0x72, 0x68 };
    static byte[] fourCCStrf = new byte[] { 0x73, 0x74, 0x72, 0x66 };
    static byte[] fourCCVids = new byte[] { 0x76, 0x69, 0x64, 0x73 };
    static byte[] fourCCDib = new byte[]  { 0x44, 0x49, 0x42, 0x20 };    
    static byte[] fourCCMovi = new byte[] { 0x6D, 0x6F, 0x76, 0x69 };
    static byte[] fourCCDSig = new byte[] { 0x30, 0x30, 0x64, 0x62 };
    static byte[] fourCCJunk = new byte[] { 0x4A, 0x55, 0x4E, 0x4B };
  }
}