angular.module('personnelDirectory', ['ngRoute', 'ngResource']);

angular.module('personnelDirectory').config(['$routeProvider', function ($routeProvider) {
	$routeProvider.
		when('/personnel', {
			controller: 'personnelListController',
			templateUrl: '/app/partials/personnel-list.html'
		}).
		when('/personnel/:id', {
			controller: 'personnelDetailsController',
			templateUrl: '/app/partials/personnel-details.html'
		}).
		when('/help', {
			controller: 'helpController',
			templateUrl: '/app/partials/help.html'
		}).
		otherwise({
			redirectTo: '/personnel'
		});
}]);
