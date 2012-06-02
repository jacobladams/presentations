using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Weather.Service
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWeatherService" in both code and config file together.
	[ServiceContract]
	public interface IWeatherService
	{

		[OperationContract]
		List<WeatherDay> GetForecast(string planet);
	}


	// Use a data contract as illustrated in the sample below to add composite types to service operations.
	[DataContract]
	public class WeatherDay
	{
		[DataMember]
		public string WeatherType {get; set;}

		[DataMember]
		public int Temperature {get; set;}

		[DataMember]
		public string DayOfTheWeek { get; set; }
	}
}
