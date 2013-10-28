///<reference path="jquery.d.ts" />

import Models = require('Models');

export class RaffleService {
    _attendees: Models.IAttendee[];
    _prizes: Models.Prize[];

    getPrizes(callback): void {
        setTimeout(function () {
            callback([
                new Models.Prize('Free 14 day trial of Office 2013'),
                new Models.Prize('Pluralsight subscription'),
                new Models.Prize('Days of .NET ticket')
            ]);
        }, 1000);
    }

    getAttendees(callback): void {
        setTimeout(function () {
            callback([
                new Models.Attendee('Moe', 'moe@hotmail.com'),
                new Models.Attendee('Larry', 'larry@geocities.com'),
                new Models.Attendee('Curly', 'curly@altavista.com'),
                new Models.RegularAttendee('Jake', 'jake@gmail.com', 'Dr. Pepper'),
                new Models.Organizer('Scott', 'scott@ms.com'),
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
                    var winner: Models.IAttendee = this._attendees.splice(winningIndex, 1)[0];

                    results.append(winner.getPrizeMessage(this._prizes[i]) + '<br/>');
                }
            });
        });
    }
}

