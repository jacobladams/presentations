///<reference path="jquery.d.ts" />
//declare var $: any;

class Prize {
    name: string;

    constructor(name: string) {
        this.name = name;
    }
}

interface IAttendee {
    getPrizeMessage(prize: Prize): string;
}

//class MockAttendee {
//    getPrizeMessage(prize: Prize): string {
//        return 'Test Message with prize named: ' + prize.name + ' was passed in';
//    }
//}

class Attendee implements IAttendee {
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
    constructor(name: string, email: string, public favoriteSoda?: string) {
        super(name, email);
    }
}

class Organizer extends RegularAttendee {
    getPrizeMessage(prize: Prize): string {
        if (prize.name === 'Days of .NET ticket') {
            return 'WTF?, ' + this.name + ', you won a ' + prize.name + '! This seems suspect.';
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
    ]
}

function getAttendees(): IAttendee[] {
    return [
        new Attendee('Moe', 'moe@hotmail.com'),
        new Attendee('Larry', 'larry@geocities.com'),
        new Attendee('Curly', 'curly@altavista.com'),
        new RegularAttendee('Jake', 'jake@gmail.com', 'Dr. Pepper'),
        new Organizer('Scott', 'scott@ms.com'),
        //new MockAttendee()
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

