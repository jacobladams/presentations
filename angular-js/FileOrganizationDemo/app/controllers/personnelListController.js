angular.module('personnelDirectory').controller('personnelListController', ['$scope', '$resource', function ($scope, $resource) {
	var personnelDirectory = {};

	var personnelResource = $resource('/api/Personnel');

	personnelDirectory.personnelList = personnelResource.query();

	$scope.personnelDirectory = personnelDirectory;
}]);
