angular.module('personnelDirectory').controller('personnelDetailsController', ['$scope', '$routeParams', 'personnelDirectory', function ($scope, $routeParams, personnelDirectory) {
	var personnelId = $routeParams.id;

	personnelDirectory.setPersonnel(personnelId);

	$scope.personnelDirectory = personnelDirectory;

}]);