///<reference path="../jquery.d.ts" />

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

function getPrizes(): Prize[] {
    return [
        new Prize('Platic Water Bottle with Conference Logo'),
        new Prize('Signed Anders Hejlsberg Photo'),
        new Prize('Golden xBox 720 Ultimate RT Pro 8 Series - 64 bit')
    ]
}

function getAttendees(): Attendee[]{
    return [
        new Attendee('Moe', 'moe@hotmail.com'),
        new Attendee('Larry', 'larry@geocities.com'),
        new Attendee('Curly', 'curly@altavista.com')
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

