using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.Automation;
using System.Windows;
using System.Windows.Controls;
using Microsoft.SharePoint.Client;

namespace FullTrust
{
	public partial class MainPage : UserControl
	{
		private ListItemCollection _items;
		private List _list;
		private ClientContext _clientContext;

		public MainPage()
		{
			InitializeComponent();

			_clientContext = new ClientContext("http://jakesharepointsaturday.sharepoint.com/TeamSite");

			Web site = _clientContext.Web;
			_list = site.Lists.GetByTitle("Important Documents");

			_items = _list.GetItems(new CamlQuery());
			_items.Include(item => item["Title"], item=>item.DisplayName);
			_clientContext.Load(_items);
			_clientContext.ExecuteQueryAsync(SuccessCallback, FailedCallback);
		}

		private void SuccessCallback(object Sender, ClientRequestSucceededEventArgs e)
		{
			Dispatcher.BeginInvoke(() =>
			{
				DocumentsList.ItemsSource = _items;
			});
		}

		private void FailedCallback(object Sender, ClientRequestFailedEventArgs e)
		{
			Dispatcher.BeginInvoke(() =>
			{
				MessageBox.Show(e.Message);
			});
		}

		private void Email_Click(object sender, RoutedEventArgs e)
		{
			// check if we can actually do this
			if (!Application.Current.HasElevatedPermissions)
			{
				MessageBox.Show("Application requires elevated trust for this!");
				return;
			}
			var listItems = DocumentsList.SelectedItems.OfType<ListItem>();
			dynamic outlook = AutomationFactory.CreateObject("Outlook.Application");
			dynamic mailItem = outlook.CreateItem(0);
			mailItem.To = "vader@example.com";
			mailItem.Subject = "Important Documents";
			mailItem.HTMLBody = "<P>Important Documents</P>";

			string tempDirectory = "c:\\temp\\FullTrustDemo";

			System.IO.Directory.CreateDirectory(tempDirectory);

			foreach (ListItem listItem in listItems)
			{
				Microsoft.SharePoint.Client.File.OpenBinaryDirect(_clientContext, listItem["FileRef"].ToString(), 
					(s, args) =>
					{
						Dispatcher.BeginInvoke(() =>
						{
							string path = string.Format("{0}\\{1}",tempDirectory, listItem["FileLeafRef"].ToString());
							using (FileStream fs = System.IO.File.Create(path))
							{
								args.Stream.CopyTo(fs);
							}

							mailItem.Attachments.Add(path, Type.Missing, 1, listItem["FileLeafRef"].ToString());
						});
					}, DownloadFailedCallback);	
			}
			mailItem.Display();	
		}

		private void Print_Click(object sender, RoutedEventArgs e)
		{
			// check if we can actually do this
			if (!Application.Current.HasElevatedPermissions)
			{
				MessageBox.Show("Application requires elevated trust for this!");
				return;
			}
			var listItems = DocumentsList.SelectedItems.OfType<ListItem>();
			foreach (ListItem listItem in listItems)
			{
				Microsoft.SharePoint.Client.File.OpenBinaryDirect(_clientContext, listItem["FileRef"].ToString(), DownloadSuccessCallback, DownloadFailedCallback);	
			}
			
		}

		private void DownloadSuccessCallback(object Sender, OpenBinarySucceededEventArgs e)
		{
			Dispatcher.BeginInvoke(() =>
			{
				using(dynamic comInteropPrinter = AutomationFactory.CreateObject("ComInteropPrinter.ComClass"))
				{
					using (MemoryStream ms = new MemoryStream())
					{
						e.Stream.CopyTo(ms);
						comInteropPrinter.PrintWordDocument(ms.ToArray(), "Document From SharePoint");
					}
				}
			});
		}

		private void DownloadFailedCallback(object Sender, OpenBinaryFailedEventArgs e)
		{
			Dispatcher.BeginInvoke(() =>
			{
				MessageBox.Show(e.Message);
			});
		}
	}
}
