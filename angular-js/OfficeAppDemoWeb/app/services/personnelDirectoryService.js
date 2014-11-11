angular.module('personnelDirectory').factory('personnelDirectory', [function () {
	//var personnelResource = $resource('/api/Personnel/:id', { id: '@id' });
	//var titleResource = $resource('/api/Title');

	var personnelDirectory = {};

	//personnelDirectory.personnelList = personnelResource.query();
	//personnelDirectory.titles = titleResource.query();


	personnelDirectory.personnelList = [
		{
			firstName: 'Lorth',
			lastName: 'Needa',
			title: 'Captain',
			image: '/images/needa.jpg',
			active: true,
			id: 5,
			detailsUrl: 'http://www.starwars.com/databank/captain-needa'
		}, {
			firstName: 'Darth',
			lastName: 'Vader',
			title: 'Sith Lord',
			image: '/images/vader.jpg',
			active: true,
			id: 2,
			detailsUrl: 'http://www.starwars.com/databank/darth-vader'
		}, {
			firstName: 'Kendal',
			lastName: 'Ozzel',
			title: 'Admiral',
			image: '/images/ozzel.jpg',
			active: true,
			id: 3,
			detailsUrl: 'http://www.starwars.com/databank/admiral-ozzel'
		}, {
			firstName: 'Firmus',
			lastName: 'Piett',
			title: 'Captain',
			image: '/images/piett.jpg',
			active: true,
			id: 4,
			detailsUrl: 'http://www.starwars.com/databank/admiral-piett'
		}, {
			firstName: 'Wilhuff',
			lastName: 'Tarkin',
			title: 'Grand Moff',
			image: '/images/tarkin.jpg',
			active: false,
			id: 1,
			detailsUrl: 'http://www.starwars.com/databank/tarkin'
		}
	];

	personnelDirectory.titles = ['Captain', 'Admiral', 'Sith Lord', 'Grand Moff'];

	personnelDirectory.setPersonnel = function(id) {
		personnelDirectory.personnel = personnelDirectory.personnelList.filter(function(personnel) {
			return personnel.id == id;
		})[0];
	};

	personnelDirectory.savePersonnel = function (personnel) {
		personnel.isEditting = false;
	};

	personnelDirectory.editPersonnel = function (personnel) {
		personnel.isEditting = true;
	};

	return personnelDirectory;
}]);