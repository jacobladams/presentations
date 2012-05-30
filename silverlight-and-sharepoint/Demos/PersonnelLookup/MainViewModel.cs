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
using System.ComponentModel;
using System.Collections.Generic;
using Microsoft.SharePoint.Client;
using System.Linq;
using System.Windows.Threading;
using System.Windows.Media.Imaging;

namespace PersonnelLookup
{
	public class Lookup
	{
		public string Name { get; set; }
		public int Id { get; set; }
	}

	public class MainViewModel : INotifyPropertyChanged
	{


		#region Properties

		private string _name;
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
				NotifyPropertyChanged("Name");
			}
		}

		private Lookup _title;
		public Lookup Title
		{
			get
			{
				return _title;
			}
			set
			{
				_title = value;
				NotifyPropertyChanged("Title");
			}
		}

		private List<Lookup> _titles;
		public List<Lookup> Titles
		{
			get
			{
				return _titles;
			}
			set
			{
				_titles = value;
				NotifyPropertyChanged("Titles");
			}
		}

		private Lookup _office;
		public Lookup Office
		{
			get
			{
				return _office;
			}
			set
			{
				_office = value;
				NotifyPropertyChanged("Office");
			}
		}

		private List<Lookup> _offices;
		public List<Lookup> Offices
		{
			get
			{
				return _offices;
			}
			set
			{
				_offices = value;
				NotifyPropertyChanged("Offices");
			}
		}

		private bool _isActive;
		public bool IsActive
		{
			get
			{
				return _isActive;
			}
			set
			{
				_isActive = value;
				NotifyPropertyChanged("IsActive");
			}
		}

		private List<PersonnelViewModel> _allPersonnel;

		private List<PersonnelViewModel> _searchResults;
		public List<PersonnelViewModel> SearchResults
		{
			get
			{
				return _searchResults;
			}
			set
			{
				_searchResults = value;
				NotifyPropertyChanged("SearchResults");
			}
		}

		private PersonnelViewModel _selectedPersonnel;
		public PersonnelViewModel SelectedPersonnel
		{
			get
			{
				return _selectedPersonnel;
			}
			set
			{
				_selectedPersonnel = value;
				NotifyPropertyChanged("SelectedPersonnel");
			}
		}

		private string _error;
		public string Error
		{
			get
			{
				return _error;
			}
			set
			{
				_error = value;
				NotifyPropertyChanged("Error");
			}
		}

		#endregion

		private void NotifyPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		private Web _site;
		private List _list;
		private ListItemCollection _items;
		private Dispatcher _dispatcher;

		public MainViewModel(Dispatcher dispatcher)
		{
			_dispatcher = dispatcher;
			ClientContext clientContext = new ClientContext("http://jakesharepointsaturday.sharepoint.com/TeamSite");
			_site = clientContext.Web;
			clientContext.Load(_site);
			_list = _site.Lists.GetByTitle("Personnel");
			_items = _list.GetItems(new CamlQuery());
			//_items.Include(item => item["Title"], item => item["First_x0020_Name"], item => item["Last_x0020_Name"], item => item["Fun_x0020_Fact"], item => item["Office"], item => item["PersonnelTitle"], item => item["Image"]);
			clientContext.Load(_items);
			clientContext.ExecuteQueryAsync(SuccessCallback, FailedCallback);
		}

		private void SuccessCallback(object Sender, ClientRequestSucceededEventArgs e)
		{
			//_allPersonnel = (from ListItem personnelListItem in _items
			//                 select new PersonnelViewModel
			//                 {
			//                     Name = personnelListItem["Name"].ToString(),
			//                     //FirstName = personnelListItem["First Name"].ToString(),
			//                     //LastName = personnelListItem["Last Name"].ToString(),
			//                 }).ToList();
			_dispatcher.BeginInvoke(() =>
				{
					_allPersonnel = new List<PersonnelViewModel>();

					foreach (var personnelListItem in _items)
					{
						var personnel = new PersonnelViewModel
						{
							Name = Convert.ToString(personnelListItem["Title"]),
							Email = Convert.ToString(personnelListItem["Email"]),
							FirstName = Convert.ToString(personnelListItem["First_x0020_Name"]),
							LastName = Convert.ToString(personnelListItem["Last_x0020_Name"]),
							
							FunFact = Convert.ToString(personnelListItem["Fun_x0020_Fact"]),
							IsActive = Convert.ToBoolean(personnelListItem["Active"]),
							ReasonForLeaving = Convert.ToString(personnelListItem["Reason_x0020_For_x0020_Leaving"]),
						};

						FieldUrlValue imageFieldValue = personnelListItem["Image"] as FieldUrlValue;
						personnel.Image = new BitmapImage(new Uri(imageFieldValue.Url, UriKind.Absolute));

						FieldLookupValue officeLookupValue = personnelListItem["Office"] as FieldLookupValue;
						personnel.Office = new Lookup
						{
							Id = officeLookupValue.LookupId,
							Name = officeLookupValue.LookupValue
						};

						FieldLookupValue titleLookupValue = personnelListItem["PersonnelTitle"] as FieldLookupValue;
						personnel.Title = new Lookup
						{
							Id = titleLookupValue.LookupId,
							Name = titleLookupValue.LookupValue
						};

						_allPersonnel.Add(personnel);
					}

					SearchResults = _allPersonnel;
				});
		}

		private void FailedCallback(object Sender, ClientRequestFailedEventArgs e)
		{
			//Dispatcher.BeginInvoke(() => test.Text = e.Message);
			//Dispatcher.BeginInvoke(() =>
			//{
			//    message.Text = e.Message;
			//});
		}
	}
}
