using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.SharePoint.Client;
using System.IO;

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
