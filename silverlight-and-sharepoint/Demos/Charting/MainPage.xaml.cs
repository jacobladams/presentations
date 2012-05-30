using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.SharePoint.Client;

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

			var displayTypes = new List<string>(){"All Data", "Boring Count", "Boring Fire Power", "Bar Chart Count", "Bar Chart Fire Power", "Pie Chart Count", "Pie Chart Fire Power", "Tree Map Count", "Tree Map Fire Power"};
			DisplayType.ItemsSource = displayTypes;
			DisplayType.SelectedItem = displayTypes.First();

			_clientContext = new ClientContext("http://jakesharepointsaturday.sharepoint.com/TeamSite");

			//PopulateListWithTestData();

			Web site = _clientContext.Web;
			_list = site.Lists.GetByTitle("Death Star Inventory 2");

			_items = _list.GetItems(new CamlQuery());
			_clientContext.Load(_items);
			_clientContext.ExecuteQueryAsync(SuccessCallback, FailedCallback);
		}

		private void PopulateListWithTestData()
		{
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

		private void DisplayType_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			//MessageBox.Show(DisplayType.SelectedValue.ToString());
			BoringGrid.Visibility = DisplayType.SelectedValue == "All Data" ? Visibility.Visible : Visibility.Collapsed;
			BoringCount.Visibility = DisplayType.SelectedValue == "Boring Count" ? Visibility.Visible : Visibility.Collapsed;
			BoringFirePower.Visibility = DisplayType.SelectedValue == "Boring Fire Power" ? Visibility.Visible : Visibility.Collapsed;
			CountBarChart.Visibility = DisplayType.SelectedValue == "Bar Chart Count" ? Visibility.Visible : Visibility.Collapsed;
			FirePowerBarChart.Visibility = DisplayType.SelectedValue == "Bar Chart Fire Power" ? Visibility.Visible : Visibility.Collapsed;
			CountPieChart.Visibility = DisplayType.SelectedValue == "Pie Chart Count" ? Visibility.Visible : Visibility.Collapsed;
			FirePowerPieChart.Visibility = DisplayType.SelectedValue == "Pie Chart Fire Power" ? Visibility.Visible : Visibility.Collapsed;
			CountTreeMap.Visibility = DisplayType.SelectedValue == "Tree Map Count" ? Visibility.Visible : Visibility.Collapsed;
			FirePowerTreeMap.Visibility = DisplayType.SelectedValue == "Tree Map Fire Power" ? Visibility.Visible : Visibility.Collapsed;
		}


	}
}
