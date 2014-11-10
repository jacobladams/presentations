angular.module('personnelDirectory').factory('personnelDirectory', ['$resource', function ($resource) {
	var personnelResource = $resource('/api/Personnel/:id', { id: '@id' });
	var titleResource = $resource('/api/Title');

	var personnelDirectory = {};

	personnelDirectory.personnelList = personnelResource.query();
	personnelDirectory.titles = titleResource.query();

	personnelDirectory.setPersonnel = function(id) {
		personnelDirectory.personnelList.$promise.then(function() {
			personnelDirectory.personnel = personnelDirectory.personnelList.filter(function(personnel) {
				return personnel.id == id;
			})[0];
		});
	};

	personnelDirectory.savePersonnel = function (personnel) {
		personnel.$save().then(function () {
			personnel.isEditting = false;
		});
	};

	personnelDirectory.editPersonnel = function (personnel) {
		personnel.isEditting = true;
	};

	return personnelDirectory;
}]);