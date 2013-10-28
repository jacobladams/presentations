///<reference path="../jquery.d.ts" />

module RaffleJS {
    export class Prize {
        name: string;

        constructor(name: string) {
            this.name = name;
        }
    }

    export interface IAttendee {
        getPrizeMessage(prize: Prize): string;
    }

    export class Attendee implements IAttendee {
        getPrizeMessage(prize: Prize): string {
            return 'Congrats, ' + this.name + '! You won a ' + prize.name;
        }
        constructor(private name: string, public email: string) {
        }
    }

    export class RegularAttendee extends Attendee {
        getPrizeMessage(prize: Prize): string {
        	if (prize.name === 'Free 14 day trial of Office 2013') {
                return 'Tough luck, ' + this.name + '! You won another ' + prize.name;
            } else {
                return super.getPrizeMessage(prize);
            }
        }
        constructor(private name: string, email: string, public favoriteBeer?: string) {
            super(name, email);
        }
    }

    export class Organizer extends RegularAttendee {
        getPrizeMessage(prize: Prize): string {
        	if (prize.name === 'Days of .NET ticket') {
                return 'WTF?, ' + this.name + ', you won a ' + prize.name + '! This seems suspect.';
            } else {
                return super.getPrizeMessage(prize);
            }
        }
        constructor(private name: string, email: string, favoriteBeer?: string) {
            super(name, email, favoriteBeer);
        }
    }

    export class RaffleService {
        _attendees: IAttendee[];
        _prizes: Prize[];

        getPrizes(callback): void {
            setTimeout(function () {
                callback([
                    new Prize('Free 14 day trial of Office 2013'),
                    new Prize('Pluralsight subscription'),
                    new Prize('Days of .NET ticket')
                ]);
            }, 1000);
        }

        getAttendees(callback): void {
            setTimeout(function () {
                callback([
                    new Attendee('Moe', 'moe@hotmail.com'),
                    new Attendee('Larry', 'larry@geocities.com'),
					new Attendee('Curly', 'curly@altavista.com'),
					new RegularAttendee('Jake', 'jake@gmail.com', 'Boulevard Tank 7'),
					new Organizer('Jonathan', 'jonathan@github.com')
                ]);
            }, 1000);
        }

        raffle() {
            this.getPrizes((prizes) => {
                this.getAttendees((attendees) => {
                    this._attendees = attendees;
                    this._prizes = prizes;

                    var numberOfAttendees = this._attendees.length;

                    var numberOfPrizes = this._prizes.length;

                    var results = $('#results');
                    results.html('');
                    for (var i = 0; i < numberOfPrizes; i++) {
                        var winningIndex = Math.floor((Math.random() * this._attendees.length));
                        var winner: IAttendee = this._attendees.splice(winningIndex, 1)[0];

                        results.append(winner.getPrizeMessage(this._prizes[i]) + '<br/>');
                    }
                });
            });

        }
    }
}
$(function () {
    $('#raffleButton').click(function () {
        var raffleService = new RaffleJS.RaffleService();
        raffleService.raffle();

    });
});

