using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.SharePoint.Client;

namespace FileUpload
{
	public partial class MainPage : UserControl
	{
		private Web _site;
		private List _list;

		public MainPage()
		{
			InitializeComponent();	
		}

		private void LayoutRoot_Drop(object sender, DragEventArgs e)
		{
			message.Text = "Item Dropped!";
			FileInfo[] droppedFiles = e.Data.GetData(DataFormats.FileDrop) as FileInfo[];

			ClientContext clientContext = new ClientContext("http://jakesharepointsaturday.sharepoint.com/TeamSite");
			_site = clientContext.Web;
			_list = _site.Lists.GetByTitle("Important Documents");
			
			foreach (FileInfo file in droppedFiles)
			{
				byte[] contents  = GetContentsOfFile(file);
				
				FileCreationInformation newFile = new FileCreationInformation();
				newFile.Content = contents;
				newFile.Url = file.Name;

				Microsoft.SharePoint.Client.File uploadFile = _list.RootFolder.Files.Add(newFile);
				clientContext.Load(uploadFile);
				clientContext.ExecuteQueryAsync(SuccessCallback, FailedCallback);
			}
		}

		private byte[] GetContentsOfFile(FileInfo file)
		{
			using (MemoryStream ms = new MemoryStream())
			{
				file.OpenRead().CopyTo(ms);
				return ms.ToArray();
			}
		}

		private void SuccessCallback(object Sender, ClientRequestSucceededEventArgs e)
		{
			Dispatcher.BeginInvoke(() => message.Text = "Done!");
		}

		private void FailedCallback(object Sender, ClientRequestFailedEventArgs e)
		{
			Dispatcher.BeginInvoke(() => message.Text = e.Message);
		}
	}
}
