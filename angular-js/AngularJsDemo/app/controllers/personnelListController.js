
angular.module('personnelDirectory').controller('personnelListController', ['$scope', function ($scope) {
	var statuses = [{
		id: 1,
		name: 'Active'
	}, {
		id: 2,
		name: 'Inactive'
	}];

	var personnelList = [{
		id: 1,
		firstName: 'Darth',
		lastName: 'Vader',
		title: 'Sith Lord',
		image: 'someurltoanimage',
		detailsUrl: 'somelinktodetails',
		status: statuses[0]
	}, {
		id: 2,
		firstName: 'Something',
		lastName: 'Ozel',
		title: 'Admiral',
		image: 'someurltoanimage',
		detailsUrl: 'somelinktodetails',
		status: statuses[0]
	}];

	$scope.statuses = statuses;

	$scope.personnelList = personnelList;

	$scope.editPersonnel = function (personnel) {
		personnel.isEditting = true;
	}

	$scope.savePersonnel = function (personnel) {
		personnel.isEditting = false;
	}

}]);
