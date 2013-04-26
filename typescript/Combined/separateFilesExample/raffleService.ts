///<reference path="../jquery.d.ts" />
///<reference path="models.ts" />

module RaffleJS.Services {
    export class RaffleService {
        _attendees: RaffleJS.Models.IAttendee[];
        _prizes: RaffleJS.Models.Prize[];

        getPrizes(callback): void {
            setTimeout(function () {
                callback([
                    new RaffleJS.Models.Prize('Succeeding with Agile book'),
                    new RaffleJS.Models.Prize('Pluralsight Subscription'),
                    new RaffleJS.Models.Prize('KCDC Ticket')
                ]);
            }, 1000);
        }

        getAttendees(callback): void {
            setTimeout(function () {
                callback([
                    new RaffleJS.Models.Attendee('Moe', 'moe@hotmail.com'),
                    new RaffleJS.Models.Attendee('Larry', 'larry@geocities.com'),
                    new RaffleJS.Models.Attendee('Curly', 'curly@altavista.com'),
                    new RaffleJS.Models.RegularAttendee('Jake', 'jake@gmail.com', 'Left Hand Haystack'),
                    new RaffleJS.Models.Organizer('Nick', 'nick@github.com'),
                ]);
            }, 1000);
        }

        raffle() {
            this.getPrizes((prizes)=> {
                this.getAttendees((attendees) =>{
                    this._attendees = attendees;
                    this._prizes = prizes;

                    var numberOfAttendees = this._attendees.length;

                    var numberOfPrizes = this._prizes.length;

                    var results = $('#results');
                    results.html('');
                    for (var i = 0; i < numberOfPrizes; i++) {
                        var winningIndex = Math.floor((Math.random() * this._attendees.length));
                        var winner: RaffleJS.Models.IAttendee = this._attendees.splice(winningIndex, 1)[0];

                        results.append(winner.getPrizeMessage(this._prizes[i]) + '<br/>');
                    }
                });
            });

        }
    }
}
