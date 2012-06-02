using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Weather.Service
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WeatherService" in code, svc and config file together.
	public class WeatherService : IWeatherService
	{
		public List<WeatherDay> GetForecast(string planet)
		{
			var forecast = new List<WeatherDay>();
			switch (planet)
			{
				case "Tatooine":
					break;
				case "Dagobah":
					break;
				case "Hoth":
					break;
				case "Alderaan":
					break;
				default:
					break;
			}
			return forecast;
		}
	}
}
