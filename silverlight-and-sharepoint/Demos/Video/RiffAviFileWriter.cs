using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.IO;
using WebCamAviSaving.Recording.RiffAviFileRecording;

namespace WebCamAviSaving.Recording
{
  public class RiffAviFileWriter
  {
    public RiffAviFileWriter(Stream stream)
    {
      this.stream = stream;
      bufferQueue = new BufferQueue();
    }
    public void StartFile(uint width, uint height, uint framesPerSecond)
    {
      this.width = width;
      this.height = height;
      this.framesPerSecond = framesPerSecond;

      bufferQueue = new BufferQueue();
      bufferQueue.WorkAvailable += OnBufferQueueWorkAvailable;
      bufferQueue.Shutdown += OnBufferQueueShutdown;

      SyncWriteFileHeader();
    }
    void OnBufferQueueShutdown(object sender, EventArgs e)
    {
      strhFrameCountBookmark.ReplaceValue(totalFrames);
      avihFrameCountBookmark.ReplaceValue(totalFrames);
      listMovi.Close();
      riffChunk.Close();
      this.stream.Flush();
      this.stream.Close();
      this.stream.Dispose();
    }
    void OnBufferQueueWorkAvailable(object sender, EventArgs e)
    {
      byte[] work = bufferQueue.GetWork();

      if (work != null)
      {
        listMovi.WriteFrameAsync(work, () =>
          {
            OnBufferQueueWorkAvailable(null, null);
          });
      }
    }
    void SyncWriteFileHeader()
    {
      riffChunk = new RiffChunk(this.stream);
      riffChunk.WriteFourCC(RiffAviFourCCCode.Avi);

      RiffChunk listHdrl = riffChunk.AddChild(RiffAviFourCCCode.List);
      listHdrl.WriteFourCC(RiffAviFourCCCode.Hdrl);

      RiffChunk avihChunk = listHdrl.AddChild(RiffAviFourCCCode.Avih);
      WriteAvihHeaderStructure(avihChunk);
      avihChunk.Close();

      RiffChunk listStrl = listHdrl.AddChild(RiffAviFourCCCode.List);
      listStrl.WriteFourCC(RiffAviFourCCCode.Strl);

      RiffChunk strhChunk = listStrl.AddChild(RiffAviFourCCCode.Strh);
      WriteStrhHeaderStructure(strhChunk);
      strhChunk.Close();

      RiffChunk strfChunk = listStrl.AddChild(RiffAviFourCCCode.Strf);
      WriteStrfHeaderStructure(strfChunk);
      strfChunk.Close();

      listStrl.Close();

      listHdrl.Close();

      listMovi = riffChunk.AddChild(RiffAviFourCCCode.List);
      listMovi.WriteFourCC(RiffAviFourCCCode.Movi);
    }
    public void WriteFrame(byte[] sampleData)
    {
      totalFrames++;
      bufferQueue.AddWork(sampleData);
    }
    public void EndFile()
    {
      bufferQueue.EndWork();
    }
    void WriteAvihHeaderStructure(RiffChunk chunk)
    {
      UInt32 microSecondsPerFrame =
        (UInt32)((1.0 / framesPerSecond) * 1.0E6);

      chunk.WriteDWORD(microSecondsPerFrame);             // Microseconds per frame
      chunk.WriteDWORD(0);                                // Max Bytes Per Second
      chunk.WriteDWORD(0);                                // Padding granularity
      chunk.WriteDWORD(0);                                // Flags

      avihFrameCountBookmark =
        chunk.WriteAndBookmarkDWORD(0);                   // Total frames

      chunk.WriteDWORD(0);                                // Initial frames
      chunk.WriteDWORD(1);                                // Streams
      chunk.WriteDWORD(width * height * 4);               // Suggested buffer size
      chunk.WriteDWORD(width);                            // Width
      chunk.WriteDWORD(height);                           // Height
      chunk.WriteDWORD(0);                                // Reserved [0]
      chunk.WriteDWORD(0);                                // Reserved [1]
      chunk.WriteDWORD(0);                                // Reserved [2]
      chunk.WriteDWORD(0);                                // Reserved [3]
    }
    void WriteStrhHeaderStructure(RiffChunk chunk)
    {
      chunk.WriteFourCC(RiffAviFourCCCode.Vids);            // 'vids'
      chunk.WriteFourCC(RiffAviFourCCCode.Dib);             // 'DIB '
      chunk.WriteDWORD(0);                                  // flags
      chunk.WriteWORD(0);                                   // priority
      chunk.WriteWORD(0);                                   // language      
      chunk.WriteDWORD(0);                                  // initial frames
      chunk.WriteDWORD(1);                                  // scale
      chunk.WriteDWORD(framesPerSecond);                    // rate
      chunk.WriteDWORD(0);                                  // start

      strhFrameCountBookmark =
        chunk.WriteAndBookmarkDWORD(0);                     // frameCount

      chunk.WriteDWORD(width * height * 4);                 // suggested buffer size
      chunk.WriteDWORD(0);                                  // quality
      chunk.WriteDWORD(0);                                  // sample size
      chunk.WriteDWORD(0);                                  // left
      chunk.WriteDWORD(0);                                  // top
      chunk.WriteDWORD((ushort)width);                      // right
      chunk.WriteDWORD((ushort)height);                     // bottom
    }
    void WriteStrfHeaderStructure(RiffChunk chunk)
    {
      chunk.WriteDWORD(40);                             // structure size - TOTAL structure size              
      chunk.WriteDWORD(width);                          // width
      chunk.WriteDWORD(height);                         // height
      chunk.WriteWORD(1);                               // planes
      chunk.WriteWORD(32);                              // bits per pixel
      chunk.WriteDWORD(0);                              // compression ( BI_RGB == 0 )
      chunk.WriteDWORD(width * height * 4);             // size image - TODO
      chunk.WriteDWORD(0);                              // X pels per meter
      chunk.WriteDWORD(0);                              // Y pels per meter
      chunk.WriteDWORD(0);                              // clr used
      chunk.WriteDWORD(0);                              // clr important 
      //NB: No colour table.
    }
    uint width;
    uint height;
    uint framesPerSecond;
    uint totalFrames;
    RiffChunk riffChunk;
    RiffChunk listMovi;
    Stream stream;
    BufferQueue bufferQueue;
    StreamBookmark avihFrameCountBookmark;
    StreamBookmark strhFrameCountBookmark;
  }
}