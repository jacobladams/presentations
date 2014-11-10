angular.module('personnelDirectory').controller('personnelListController', ['$scope', '$http', '$resource', function ($scope, $http, $resource) {
	var personnelDirectory = {};

	var personnelResource = $resource('/api/Personnel');

	personnelDirectory.personnelList = personnelResource.query();

	$scope.personnelDirectory = personnelDirectory;
}]);
