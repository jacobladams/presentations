angular.module('personnelDirectory').controller('personnelDetailsController', ['$scope', '$routeParams', '$http', '$resource', function ($scope, $routeParams, $http, $resource) {
	var personnelId = $routeParams.id;
	var personnelDirectory = {};

	var personnelResource = $resource('/api/Personnel/:id', { id: 'id' });
	var titleResource = $resource('/api/Title');

	personnelDirectory.personnel = personnelResource.get({ id: personnelId });
	personnelDirectory.titles = titleResource.query();

	personnelDirectory.savePersonnel = function (personnel) {
		personnel.$save().then(function (savedPersonnel) {
			personnel.isEditting = false;
		});
	};

	personnelDirectory.editPersonnel = function (personnel) {
		personnel.isEditting = true;
	};

	$scope.personnelDirectory = personnelDirectory;

}]);