///<reference path="jquery.d.ts" />
//declare var $: any;

class Prize {
    name: string;

    constructor(name: string) {
        this.name = name;
    }
}

class Attendee {
    getPrizeMessage(prize: Prize): string {
        return 'Congrats, ' + this.name + '! You won a ' + prize.name;
    }
    constructor(public name: string, private email: string) {
    }
}

class RegularAttendee extends Attendee {
    getPrizeMessage(prize: Prize): string {
        if (prize.name === 'Free 14 day trial of Office 2013') {
            return 'Tough luck, ' + this.name + '! You won another ' + prize.name;
        } else {
            return super.getPrizeMessage(prize);
        }
    }
    constructor(name: string, email: string, public favoriteSoda: string = 'bud light') {
        super(name, email);
    }
}

class Organizer extends RegularAttendee {
    getPrizeMessage(prize: Prize): string {
        if (prize.name === 'Days of .NET ticket') {
            return 'WTF?, ' + this.name + ', You won a ' + prize.name + '! This seems suspect.';
        } else {
            return super.getPrizeMessage(prize);
        }
    }
    constructor(name: string, email: string, favoriteSoda?: string) {
        super(name, email, favoriteSoda);
    }
}

function getPrizes(): Prize[] {
	return [
		new Prize('Free 14 day trial of Office 2013'),
		new Prize('Pluralsight subscription'),
		new Prize('Days of .NET ticket')
	];
}

function getAttendees(): Attendee[] {
	return [
		new Attendee('Moe', 'moe@hotmail.com'),
		new Attendee('Larry', 'larry@geocities.com'),
		new Attendee('Curly', 'curly@altavista.com'),
		new RegularAttendee('Jake', 'jake@gmail.com', 'Dr. Pepper'),
		new Organizer('Scott', 'scott@ms.com')
	];
}

function selectRandomItemAndRemove<T>(items: T[]): T {
    var selectedIndex = Math.floor((Math.random() * items.length));
    var selectedItem: T = items.splice(selectedIndex, 1)[0];
    return selectedItem;
}

$(function () {
    $('#raffleButton').click(function () {
        var attendees: Attendee[] = getAttendees();

        var prizes: Prize[] = getPrizes();

        var numberOfAttendees = attendees.length;

        var numberOfPrizes = prizes.length;

        var results = $('#results');
        results.html('');
        for (var i = 0; i < numberOfPrizes; i++) {
            var winningIndex = Math.floor((Math.random() * attendees.length));
            var winner: Attendee = attendees.splice(winningIndex, 1)[0];

			//var winner = selectRandomItemAndRemove<Attendee>(attendees);

            results.append(winner.getPrizeMessage(prizes[i]) + '<br/>');
        }

    });
});

