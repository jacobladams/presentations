///<reference path="jquery.d.ts" />
define(["require", "exports", 'Models'], function(require, exports, Models) {
    var RaffleService = (function () {
        function RaffleService() {
        }
        RaffleService.prototype.getPrizes = function (callback) {
            setTimeout(function () {
                callback([
                    new Models.Prize('Free 14 day trial of Office 2013'),
                    new Models.Prize('Pluralsight subscription'),
                    new Models.Prize('Days of .NET ticket')
                ]);
            }, 1000);
        };

        RaffleService.prototype.getAttendees = function (callback) {
            setTimeout(function () {
                callback([
                    new Models.Attendee('Moe', 'moe@hotmail.com'),
                    new Models.Attendee('Larry', 'larry@geocities.com'),
                    new Models.Attendee('Curly', 'curly@altavista.com'),
                    new Models.RegularAttendee('Jake', 'jake@gmail.com', 'Dr. Pepper'),
                    new Models.Organizer('Scott', 'scott@ms.com')
                ]);
            }, 1000);
        };

        RaffleService.prototype.raffle = function () {
            var _this = this;
            this.getPrizes(function (prizes) {
                _this.getAttendees(function (attendees) {
                    _this._attendees = attendees;
                    _this._prizes = prizes;

                    var numberOfAttendees = _this._attendees.length;

                    var numberOfPrizes = _this._prizes.length;

                    var results = $('#results');
                    results.html('');
                    for (var i = 0; i < numberOfPrizes; i++) {
                        var winningIndex = Math.floor((Math.random() * _this._attendees.length));
                        var winner = _this._attendees.splice(winningIndex, 1)[0];

                        results.append(winner.getPrizeMessage(_this._prizes[i]) + '<br/>');
                    }
                });
            });
        };
        return RaffleService;
    })();
    exports.RaffleService = RaffleService;
});
//# sourceMappingURL=RaffleService.js.map
