using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.SharePoint.Client;
using System.Xml.Linq;

namespace Charting
{
	public partial class MainPage : UserControl
	{
		private ListItemCollection _items;
		private List _list;
		private ClientContext _clientContext;

		public MainPage()
		{
			InitializeComponent();

			var displayTypes = new List<string>() { "All Data", "Boring Count", "Boring Fire Power", "Bar Chart Count", "Bar Chart Fire Power", "Pie Chart Count", "Pie Chart Fire Power", "Tree Map Count", "Tree Map Fire Power" };
			DisplayType.ItemsSource = displayTypes;
			DisplayType.SelectedItem = displayTypes.First();

			//PopulateListWithTestData();
			GetListItemsClientObjectModel();
			//GetListItemsService();
		}

		#region Client Object Model

		private void GetListItemsClientObjectModel()
		{
			_clientContext = new ClientContext("http://jakesharepointsaturday.sharepoint.com/TeamSite");

			Web site = _clientContext.Web;
			_list = site.Lists.GetByTitle("Death Star Inventory 2");

			_items = _list.GetItems(new CamlQuery());
			//_clientContext.Load(_items);
			_clientContext.Load(_items, items => items.Include(i => i["Item_x0020_Type"], i => i["Fire_x0020_Power"], i => i["Title"]));

			_clientContext.ExecuteQueryAsync(SuccessCallback, FailedCallback);
		}

		private void SuccessCallback(object Sender, ClientRequestSucceededEventArgs e)
		{
			Dispatcher.BeginInvoke(() =>
			{
				BoringGrid.ItemsSource = _items;

				var itemsGroupedByType = from i in _items.ToList()
										 group i by i["Item_x0020_Type"].ToString() into g
										 select g;

				var countData = from g in itemsGroupedByType
								select new KeyValuePair<string, int>(g.Key, g.Count());

				var firePowerData = (from g in itemsGroupedByType
									 select new KeyValuePair<string, int>(g.Key, g.Sum(x => Convert.ToInt32(x["Fire_x0020_Power"]))));

				BoringCount.ItemsSource = countData;
				CountBarChart.DataContext = countData;
				CountPieChart.DataContext = countData;
				CountTreeMap.ItemsSource = countData;

				BoringFirePower.ItemsSource = firePowerData;
				FirePowerBarChart.DataContext = firePowerData;
				FirePowerPieChart.DataContext = firePowerData;
				FirePowerTreeMap.ItemsSource = firePowerData;
			});
		}

		private void FailedCallback(object Sender, ClientRequestFailedEventArgs e)
		{
			Dispatcher.BeginInvoke(() =>
			{
				MessageBox.Show(e.Message);
			});
		}

		#endregion

		#region List Service

		private void GetListItemsService()
		{
			SharePointListService.ListsSoapClient listService = new SharePointListService.ListsSoapClient();
			listService.GetListItemsCompleted += listService_GetListItemsCompleted;

			XDocument xmlDoc = new XDocument();

			XElement viewFields = new XElement("ViewFields", "<FieldRef Name='Item_x0020_Type' />" +
								   "<FieldRef Name='Fire_x0020_Power' />");

			XElement queryOptions = new XElement("QueryOptions", "<IncludeMandatoryColumns>FALSE</IncludeMandatoryColumns>");

			listService.GetListItemsAsync("Death Star Inventory 2", "", null, viewFields, null, queryOptions, null, null);
		}

		private void listService_GetListItemsCompleted(object sender, SharePointListService.GetListItemsCompletedEventArgs e)
		{
			Dispatcher.BeginInvoke(() =>
			{
				if (e.Error != null)
				{
					MessageBox.Show(e.Error.Message);
				}
				else
				{
					var itemsGroupedByType = from i in e.Result.Descendants("z:row")
											 group i by i.Attribute("Item_x0020_Type").ToString() into g
											 select g;

					var countData = from g in itemsGroupedByType
									select new KeyValuePair<string, int>(g.Key, g.Count());

					var firePowerData = (from g in itemsGroupedByType
					                     select new KeyValuePair<string, int>(g.Key, g.Sum(x => Convert.ToInt32(x.Attribute("Fire_x0020_Power")))));

					BoringCount.ItemsSource = countData;
					CountBarChart.DataContext = countData;
					CountPieChart.DataContext = countData;
					CountTreeMap.ItemsSource = countData;

					BoringFirePower.ItemsSource = firePowerData;
					FirePowerBarChart.DataContext = firePowerData;
					FirePowerPieChart.DataContext = firePowerData;
					FirePowerTreeMap.ItemsSource = firePowerData;
				}
			});
		}

		#endregion

		private void PopulateListWithTestData()
		{
			_clientContext = new ClientContext("http://jakesharepointsaturday.sharepoint.com/TeamSite");

			Web site = _clientContext.Web;
			_list = site.Lists.GetByTitle("Death Star Inventory 2");

			for (int j = 0; j < 20; j++)
			{
				for (int i = 0; i < 100; i++)
				{
					var newItemInfo = new ListItemCreationInformation() { LeafName = string.Format("TIE Fighter #{0}", i + (j * 100)) };
					ListItem newItem = _list.AddItem(newItemInfo);
					_clientContext.Load(newItem);
					newItem["Item_x0020_Type"] = "TIE Fighter";
					newItem["Fire_x0020_Power"] = 1;
					newItem.Update();
				}
				_clientContext.ExecuteQueryAsync(SuccessCallback, FailedCallback);
			}

			for (int j = 0; j < 10; j++)
			{
				for (int i = 0; i < 100; i++)
				{
					var newItemInfo = new ListItemCreationInformation() { LeafName = string.Format("Turbo Laser Battery #{0}", i + (j * 100)) };
					ListItem newItem = _list.AddItem(newItemInfo);
					_clientContext.Load(newItem);
					newItem["Item_x0020_Type"] = "Turbo Laser";
					newItem["Fire_x0020_Power"] = 4;
					newItem.Update();
				}
				_clientContext.ExecuteQueryAsync(SuccessCallback, FailedCallback);
			}
		}

		private void DisplayType_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			BoringGrid.Visibility = DisplayType.SelectedValue.ToString() == "All Data" ? Visibility.Visible : Visibility.Collapsed;
			BoringCount.Visibility = DisplayType.SelectedValue.ToString() == "Boring Count" ? Visibility.Visible : Visibility.Collapsed;
			BoringFirePower.Visibility = DisplayType.SelectedValue.ToString() == "Boring Fire Power" ? Visibility.Visible : Visibility.Collapsed;
			CountBarChart.Visibility = DisplayType.SelectedValue.ToString() == "Bar Chart Count" ? Visibility.Visible : Visibility.Collapsed;
			FirePowerBarChart.Visibility = DisplayType.SelectedValue.ToString() == "Bar Chart Fire Power" ? Visibility.Visible : Visibility.Collapsed;
			CountPieChart.Visibility = DisplayType.SelectedValue.ToString() == "Pie Chart Count" ? Visibility.Visible : Visibility.Collapsed;
			FirePowerPieChart.Visibility = DisplayType.SelectedValue.ToString() == "Pie Chart Fire Power" ? Visibility.Visible : Visibility.Collapsed;
			CountTreeMap.Visibility = DisplayType.SelectedValue.ToString() == "Tree Map Count" ? Visibility.Visible : Visibility.Collapsed;
			FirePowerTreeMap.Visibility = DisplayType.SelectedValue.ToString() == "Tree Map Fire Power" ? Visibility.Visible : Visibility.Collapsed;
		}
	}
}
