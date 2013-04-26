///<reference path="../jquery.d.ts" />
//declare var $: any;

class Prize {
    name: string;

    constructor(name: string) {
        this.name = name;
    }
}

interface IAttendee {
    getPrizeMessage(prize: Prize):string;
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

class RaffleService {
    _attendees: IAttendee[];
    _prizes: Prize[];

    getPrizes(): Prize[] {
        return [
            new Prize('Platic Water Bottle with Conference Logo'),
            new Prize('Signed Anders Hejlsberg Photo'),
            new Prize('Golden xBox 720 Ultimate RT Pro 8 Series - 64 bit')
        ]
    }

    getAttendees(): IAttendee[] {
        return [
            new Attendee('Moe', 'moe@hotmail.com'),
            new Attendee('Larry', 'larry@geocities.com'),
            new Attendee('Curly', 'curly@altavista.com'),
            new RegularAttendee('Jake', 'jake@gmail.com', 'Boulevard Tank 7'),
            new Organizer('Jonathan', 'jonathan@github.com')
        ]
    }

    raffle() {
        this._attendees = this.getAttendees();
        this._prizes = this.getPrizes();

        var numberOfAttendees = this._attendees.length;

        var numberOfPrizes = this._prizes.length;

        var results = $('#results');
        results.html('');
        for (var i = 0; i < numberOfPrizes; i++) {
            var winningIndex = Math.floor((Math.random() * this._attendees.length));
            var winner: IAttendee = this._attendees.splice(winningIndex, 1)[0];

            results.append(winner.getPrizeMessage(this._prizes[i]) + '<br/>');
        }
    }
}

$(function () {
    $('#raffleButton').click(function () {
        var raffleService = new RaffleService();
        raffleService.raffle();

    });
});

