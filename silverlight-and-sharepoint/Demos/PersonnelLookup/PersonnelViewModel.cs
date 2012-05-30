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
using System.Windows.Media.Imaging;

namespace PersonnelLookup
{
	public class PersonnelViewModel:INotifyPropertyChanged
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

		private string _firstName;
		public string FirstName 
		{
			get
			{
				return _firstName;
			}
			set
			{
				_firstName = value;
				NotifyPropertyChanged("FirstName");
			}
		}

		private string _lastName;
		public string LastName
		{
			get
			{
				return _lastName;
			}
			set
			{
				_lastName = value;
				NotifyPropertyChanged("LastName");
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

		private string _reasonForLeaving;
		public string ReasonForLeaving
		{
			get
			{
				return _reasonForLeaving;
			}
			set
			{
				_reasonForLeaving = value;
				NotifyPropertyChanged("ReasonForLeaving");
			}
		}

		private BitmapImage _image;
		public BitmapImage Image
		{
			get
			{
				return _image;
			}
			set
			{
				_image = value;
				NotifyPropertyChanged("Image");
			}
		}

		private string _funFact;
		public string FunFact
		{
			get
			{
				return _funFact;
			}
			set
			{
				_funFact = value;
				NotifyPropertyChanged("FunFact");
			}
		}

		private string _email;
		public string Email
		{
			get
			{
				return _email;
			}
			set
			{
				_email = value;
				NotifyPropertyChanged("Email");
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
	}
}
