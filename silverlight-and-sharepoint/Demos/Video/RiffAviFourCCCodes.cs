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
using System.Collections.Generic;

namespace WebCamAviSaving.Recording.RiffAviFileRecording
{
  public enum RiffAviFourCCCode
  {
    Riff,
    Avi,
    List,
    Hdrl,
    Avih,
    Strl,
    Strh,
    Strf,
    Vids,
    Dib,
    Movi,
    DSig,
    Junk
  }
  public static class RiffAviFourCCCodes
  {
    public static byte[] GetCode(RiffAviFourCCCode code)
    {
      return (codes[code]);
    }
    static Dictionary<RiffAviFourCCCode, byte[]> codes = new Dictionary<RiffAviFourCCCode,byte[]>()
    {
      { RiffAviFourCCCode.Riff, new byte[] { 0x52, 0x49, 0x46, 0x46 } },
      { RiffAviFourCCCode.Avi, new byte[] { 0x41, 0x56, 0x49, 0x20 } },
      { RiffAviFourCCCode.List, new byte[] { 0x4C, 0x49, 0x53, 0x54 } },
      { RiffAviFourCCCode.Hdrl, new byte[] { 0x68, 0x64, 0x72, 0x6C } },
      { RiffAviFourCCCode.Avih, new byte[] { 0x61, 0x76, 0x69, 0x68 } },
      { RiffAviFourCCCode.Strl, new byte[] { 0x73, 0x74, 0x72, 0x6C } },
      { RiffAviFourCCCode.Strh, new byte[] { 0x73, 0x74, 0x72, 0x68 } },
      { RiffAviFourCCCode.Strf, new byte[] { 0x73, 0x74, 0x72, 0x66 } },
      { RiffAviFourCCCode.Vids, new byte[] { 0x76, 0x69, 0x64, 0x73 } },
      { RiffAviFourCCCode.Dib, new byte[] { 0x44, 0x49, 0x42, 0x20 } },
      { RiffAviFourCCCode.Movi, new byte[] { 0x6D, 0x6F, 0x76, 0x69 } },
      { RiffAviFourCCCode.DSig, new byte[] { 0x30, 0x30, 0x64, 0x62 } },
      { RiffAviFourCCCode.Junk, new byte[] { 0x4A, 0x55, 0x4E, 0x4B } }
    };
  }
}