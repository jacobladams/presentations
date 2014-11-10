using System;
using System.Web.Http;
using System.Web.Optimization;

using AngularDemo.Models;

namespace AngularDemo
{
	public class Global : System.Web.HttpApplication
	{
		public static PersonnelRepository PersonnelRepository { get; set; }

		protected void Application_Start(object sender, EventArgs e)
		{
			GlobalConfiguration.Configure(WebApiConfig.Register);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			PersonnelRepository = new PersonnelRepository();
		}
	}
}