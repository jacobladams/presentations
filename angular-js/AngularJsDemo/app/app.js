angular.module('personnelDirectory', ['ngRoute']);

angular.module('personnelDirectory').config('$routeProvider', function ($routeProvider) {
	$routeProvider.
		when('/personnel', {
			controller: 'personnelListController',
			templateUrl: 'personnel-list'
		}).
		when('/personnel/:id', {
			controller: 'personnelController',
			templateUrl: 'personnel'
		});
});
