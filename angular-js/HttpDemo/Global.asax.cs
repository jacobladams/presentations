using System;
using System.Collections.Generic;
using System.Web.Http;

using AngularDemo.Models;


namespace AngularDemo
{
	public class Global : System.Web.HttpApplication
	{
		public static PersonnelRepository PersonnelRepository{ get; set; }

		protected void Application_Start(object sender, EventArgs e)
		{
			GlobalConfiguration.Configure(WebApiConfig.Register);

			PersonnelRepository = new PersonnelRepository();
		}
	}
}