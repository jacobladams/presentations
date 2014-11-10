using System.Web.Optimization;

namespace AngularDemo
{
	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/js").Include(
						"~/Scripts/angular.js",
						"~/Scripts/angular-route.js",
						"~/Scripts/angular-resource.js",
						"~/app/app.js",
						"~/app/services/personnelDirectoryService.js",
						"~/app/directives/personnelDetailsRead.js",
						"~/app/directives/personnelDetailsEdit.js",
						"~/app/directives/navbar.js",
						"~/app/controllers/personnelListController.js",
						"~/app/controllers/personnelDetailsController.js",
						"~/app/controllers/helpController.js"));
		}
	}
}