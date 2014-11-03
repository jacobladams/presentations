///<reference path="linq.d.ts" />

interface IAttendee {
	firstName: string;
	lastName: string;
	email: string;
	age: number;
	isRegistered: boolean;
}

var attendees: IAttendee[] = [
	{
		firstName: 'Jake',
		lastName: 'Adams',
		email: 'jacobladams@gmail.com',
		age: 30,
		isRegistered: true
	},
	{
		firstName: 'Larry',
		lastName: 'Fine',
		email: 'larry@geocities.com',
		age: 111,
		isRegistered: true
	},
	{
		firstName: 'Moe',
		lastName: 'Howard',
		email: 'moe@hotmail.com',
		age: 116,
		isRegistered: false
	},
	{
		firstName: 'Curly',
		lastName: 'Howard',
		email: 'curly@altavista.com',
		age: 110,
		isRegistered: true
	},
	{
		firstName: 'Shemp',
		lastName: 'Howard',
		email: 'shemp@excite.com',
		age: 118,
		isRegistered: false
	},
	{
		firstName: 'Nick',
		lastName: 'Adams',
		email: 'nick@github.com',
		age: 34,
		isRegistered: false
	}
];

var documentBody = document.getElementsByTagName('body')[0];

//var registeredEmailAddresses = Enumerable.from(attendees)
//	.where(function (a) { return a.isRegistered; })
//	.select(function (a) { return a.email; });

//registeredEmailAddresses.forEach(function (a) { document.write(a + '<br/>'); });


var registeredEmailAddresses = Enumerable.from(attendees)
	.where(a => a.isRegistered)
	.select(a => a.email);

registeredEmailAddresses.forEach(a => document.write(a + '<br/>'));



