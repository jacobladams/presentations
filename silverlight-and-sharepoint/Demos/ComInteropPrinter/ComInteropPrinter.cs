using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Words;
using Aspose.Words.Rendering;

namespace ComInteropPrinter
{
	[ProgId("ComInteropPrinter.ComClass")]
	[Guid("50D003D2-F68D-4944-B264-24079D4639A8")]
	public class ComClass
	{
		[ComVisible(true)]
		public void PrintWordDocument(byte[] fileContents, string documentName)
		{
			using (MemoryStream ms = new MemoryStream(fileContents))
			{
				var doc = new Document(ms);
				var printDocument = new AsposeWordsPrintDocument(doc);
				printDocument.DocumentName = documentName;
				printDocument.Print();
			}
		}
	}
}
