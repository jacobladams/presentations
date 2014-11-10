angular.module('personnelDirectory').controller('personnelListController', ['$scope', 'personnelDirectory', function ($scope, personnelDirectory) {
	$scope.personnelDirectory = personnelDirectory;
}]);
