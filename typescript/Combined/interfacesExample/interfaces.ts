///<reference path="../jquery.d.ts" />

class Prize {
    name: string;

    constructor(name: string) {
        this.name = name;
    }
}

interface IAttendee {
    getPrizeMessage(prize: Prize): string;
}

class MockAttendee {
    getPrizeMessage(prize: Prize): string {
        return 'Test Message with prize named: ' + prize.name + ' was passed in';
    }
}

class Attendee implements IAttendee {
    getPrizeMessage(prize: Prize): string {
        return 'Congrats, ' + this.name + '! You won a ' + prize.name;
    }
    constructor(private name: string, public email: string) {
    }
}

class RegularAttendee extends Attendee {
    getPrizeMessage(prize: Prize): string {
        if (prize.name === 'Platic Water Bottle with Conference Logo') {
            return 'Tough luck, ' + this.name + '! You won another ' + prize.name;
        } else {
            return super.getPrizeMessage(prize);
        }
    }
    constructor(private name: string, email: string, public favoriteBeer?: string) {
        super(name, email);
    }
}

class Organizer extends RegularAttendee {
    getPrizeMessage(prize: Prize): string {
        if (prize.name === 'Golden xBox 720 Ultimate RT Pro 8 Series - 64 bit') {
            return 'WTF?, ' + this.name + ', you won a ' + prize.name + '! This seems suspect.';
        } else {
            return super.getPrizeMessage(prize);
        }
    }
    constructor(private name: string, email: string, favoriteBeer?: string) {
        super(name, email, favoriteBeer);
    }
}

function getPrizes(): Prize[] {
    return [
        new Prize('Platic Water Bottle with Conference Logo'),
        new Prize('Signed Anders Hejlsberg Photo'),
        new Prize('Golden xBox 720 Ultimate RT Pro 8 Series - 64 bit')
    ]
}

function getAttendees(): IAttendee[] {
    return [
        new Attendee('Moe', 'moe@hotmail.com'),
        new Attendee('Larry', 'larry@geocities.com'),
        new Attendee('Curly', 'curly@altavista.com'),
        new RegularAttendee('Jake', 'jake@gmail.com', 'Boulevard Tank 7'),
	    new Organizer('Jonathan', 'jonathan@github.com')
        new MockAttendee()
    ]
}

$(function () {
    $('#raffleButton').click(function () {
        var attendees: IAttendee[] = getAttendees();

        var prizes: Prize[] = getPrizes();

        var numberOfAttendees = attendees.length;

        var numberOfPrizes = prizes.length;

        var results = $('#results');
        results.html('');
        for (var i = 0; i < numberOfPrizes; i++) {
            var winningIndex = Math.floor((Math.random() * attendees.length));
            var winner: IAttendee = attendees.splice(winningIndex, 1)[0];

            results.append(winner.getPrizeMessage(prizes[i]) + '<br/>');
        }

    });
});

