using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using GalaSoft.MvvmLight.Command;
using Microsoft.SharePoint.Client;

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

		public ICommand Search { get; set; }

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
		List _personnelList;
		private ListItemCollection _personnel;
		private ListItemCollection _officeSPItems;
		private ListItemCollection _titlesSPItems;
		private Dispatcher _dispatcher;

		public MainViewModel(Dispatcher dispatcher)
		{
			_dispatcher = dispatcher;

			Search = new RelayCommand(SearchHandler);

			ClientContext clientContext = new ClientContext("http://jakesharepointsaturday.sharepoint.com/TeamSite");
			_site = clientContext.Web;
			_personnelList = _site.Lists.GetByTitle("Personnel");
			_personnel = _personnelList.GetItems(new CamlQuery());
			//_items.Include(item => item["Title"], item => item["First_x0020_Name"], item => item["Last_x0020_Name"], item => item["Fun_x0020_Fact"], item => item["Office"], item => item["PersonnelTitle"], item => item["Image"]);
			clientContext.Load(_personnel);
			//clientContext.ExecuteQueryAsync(SuccessCallback, FailedCallback);

			List officeList = _site.Lists.GetByTitle("Office");
			_officeSPItems = officeList.GetItems(new CamlQuery());
			clientContext.Load(_officeSPItems);

			List titleList = _site.Lists.GetByTitle("Titles");
			_titlesSPItems = titleList.GetItems(new CamlQuery());
			clientContext.Load(_titlesSPItems);

			clientContext.ExecuteQueryAsync(SuccessCallback, FailedCallback);
		}

		private void SuccessCallback(object Sender, ClientRequestSucceededEventArgs e)
		{
			_dispatcher.BeginInvoke(() =>
			{
				PopulateOffices();
				PopulateTitles();
				PopulatePersonnel();
			});
		}

		private void FailedCallback(object Sender, ClientRequestFailedEventArgs e)
		{
			_dispatcher.BeginInvoke(() =>
			{
				Error = e.Message;
			});
		}

		private void PopulateOffices()
		{

			List<Lookup> tempOffices = (from o in _officeSPItems.ToList()
					   select new Lookup
					   {
						   Id = o.Id,
						   Name = Convert.ToString(o["Title"])
					   }).ToList();

			tempOffices.Insert(0, new Lookup { Id = -1, Name = " " });

			Offices = tempOffices;

			Office = Offices.First();
		}

		private void PopulateTitles()
		{
			List<Lookup> tempTitles = (from t in _titlesSPItems.ToList()
					  select new Lookup
					  {
						  Id = t.Id,
						  Name = Convert.ToString(t["Title"])
					  }).ToList();

			tempTitles.Insert(0, new Lookup { Id = -1, Name = " " });

			Titles = tempTitles;

			Title = Titles.First();
		}

		private void PopulatePersonnel()
		{
			_allPersonnel = new List<PersonnelViewModel>();

			foreach (var personnelListItem in _personnel)
			{
				var personnel = new PersonnelViewModel
				{
					Name = Convert.ToString(personnelListItem["Title"]),
					Email =  Convert.ToString(personnelListItem["Email"]),
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
		}

		private void SearchHandler()
		{
			SearchResults = (from p in _allPersonnel
							 where (String.IsNullOrWhiteSpace(Name) || p.Name.ToLower().Contains(Name.ToLower()))
							 && (Title.Id == -1 || p.Title.Id == Title.Id)
							 && (Office.Id == -1 || p.Office.Id == Office.Id)
							 && IsActive == p.IsActive
							 select p).ToList();

		}
	}
}
