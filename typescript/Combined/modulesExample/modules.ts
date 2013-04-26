///<reference path="../jquery.d.ts" />

module JakesRaffleService {
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
            if (prize.name === 'Succeeding with Agile book') {
                return 'Tough luck, ' + this.name + '! You won another copy of ' + prize.name;
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
            if (prize.name === 'KCDC Ticket') {
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
                    new Prize('Succeeding with Agile book'),
                    new Prize('Pluralsight Subscription'),
                    new Prize('KCDC Ticket')
                ]);
            }, 1000);
        }

        getAttendees(callback): void {
            setTimeout(function () {
                callback([
                    new Attendee('Moe', 'moe@hotmail.com'),
                    new Attendee('Larry', 'larry@geocities.com'),
                    new Attendee('Curly', 'curly@altavista.com'),
                    new RegularAttendee('Jake', 'jake@gmail.com', 'Left Hand Haystack'),
                    new Organizer('Nick', 'nick@github.com'),
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
        var raffleService = new JakesRaffleService.RaffleService();
        raffleService.raffle();

    });
});

