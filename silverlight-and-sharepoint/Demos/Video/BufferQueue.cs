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
using System.Collections.Generic;

namespace WebCamAviSaving
{
  public class BufferQueue
  {
    public event EventHandler WorkAvailable;
    public event EventHandler Shutdown;

    public BufferQueue()
    {
      lockObject = new object();
      queue = new Queue<byte[]>();
      workFoundEmpty = true;
    }
    public void AddWork(byte[] buffer)
    {
      bool raiseEvent = false;

      lock (queue)
      {
        if (workFoundEmpty && (queue.Count == 0))
        {
          raiseEvent = true;
          workFoundEmpty = false;
        }
        queue.Enqueue(buffer);
      }
      if (raiseEvent)
      {
        FireWorkAvailable();
      }
    }
    public byte[] GetWork()
    {
      byte[] work = null;

      lock (queue)
      {
        if (queue.Count == 0)
        {
          workFoundEmpty = true;
        }
        else
        {
          workFoundEmpty = false;
          work = queue.Dequeue();
        }
      }
      if (workFoundEmpty && workOver)
      {
        FireShutdown();
      }
      return (work);
    }
    public void EndWork()
    {
      workOver = true;

      if (workFoundEmpty)
      {
        Shutdown(this, EventArgs.Empty);
      }
    }
    void FireShutdown()
    {
      if (Shutdown != null)
      {
        Shutdown(this, EventArgs.Empty);
      }
    }
    void FireWorkAvailable()
    {
      if (WorkAvailable != null)
      {
        WorkAvailable(this, EventArgs.Empty);
      }
    }
    volatile bool workOver;
    volatile bool workFoundEmpty;
    object lockObject;
    Queue<byte[]> queue;
  }
}