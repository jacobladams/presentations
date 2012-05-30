using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
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
			//var files = System.IO.Directory.GetFiles(folderPath, "*.png");

			//var photoPage = new PhotoPage();
			//var images = files.Select(f => new BitmapImage(new Uri(f))).ToList();
			//photoPage.Image1.Source = images[0];
			//photoPage.Image2.Source = images[1];
			//photoPage.Image3.Source = images[2];
			//photoPage.Image4.Source = images[3];
			////photoPage.lstPhotos.ItemsSource = files.Select(f => new BitmapImage(new Uri(f))).ToList();

			//photoPage.Show();
			//photoPage.Hide();

			//PrintDialog pd = new PrintDialog();
			//pd.PrintVisual(photoPage.LayoutRoot, "Photobooth");
			////pd.PrintVisual(photoPage.LayoutRoot, "Photobooth");
			//fir

		}
	}
}
