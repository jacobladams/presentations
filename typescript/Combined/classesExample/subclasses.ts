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
    constructor(private name: string, public email: string) {
    }
}

class RegularAttendee extends Attendee {
    getPrizeMessage(prize: Prize): string {
        if (prize.name === 'Succeeding with Agile book') {
            return 'Tough luck, ' + this.name + '! You won another copy of ' + prize.name;
        } else {
            return super.getPrizeMessage(prize);
        } 
    }
    constructor(private name: string, email: string, public favoriteBeer: string = 'bud light') {
        super(name, email);
    }
}

class Organizer extends RegularAttendee {
    getPrizeMessage(prize: Prize): string {
        if (prize.name === 'KCDC Ticket') {
            return 'WTF?, ' + this.name + ', You won a ' + prize.name + '! This seems suspect.';
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
        new Prize('Succeeding with Agile book'),
        new Prize('Pluralsight Subscription'),
        new Prize('KCDC Ticket')
    ]
}

function getAttendees(): Attendee[] {
    return [
        new Attendee('Moe', 'moe@hotmail.com'),
        new Attendee('Larry', 'larry@geocities.com'),
        new Attendee('Curly', 'curly@altavista.com'),
        new RegularAttendee('Jake', 'jake@gmail.com', 'Left Hand Haystack'),
        new Organizer('Nick', 'nick@github.com')
    ]
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

            results.append(winner.getPrizeMessage(prizes[i]) + '<br/>');
        }

    });
});
