///<reference path="../jquery.d.ts" />
var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var RaffleJS;
(function (RaffleJS) {
    var Prize = (function () {
        function Prize(name) {
            this.name = name;
        }
        return Prize;
    })();
    RaffleJS.Prize = Prize;

    var Attendee = (function () {
        function Attendee(name, email) {
            this.name = name;
            this.email = email;
        }
        Attendee.prototype.getPrizeMessage = function (prize) {
            return 'Congrats, ' + this.name + '! You won a ' + prize.name;
        };
        return Attendee;
    })();
    RaffleJS.Attendee = Attendee;

    var RegularAttendee = (function (_super) {
        __extends(RegularAttendee, _super);
        function RegularAttendee(name, email, favoriteSoda) {
            _super.call(this, name, email);
            this.favoriteSoda = favoriteSoda;
        }
        RegularAttendee.prototype.getPrizeMessage = function (prize) {
            if (prize.name === 'Free 14 day trial of Office 2013') {
                return 'Tough luck, ' + this.name + '! You won another ' + prize.name;
            } else {
                return _super.prototype.getPrizeMessage.call(this, prize);
            }
        };
        return RegularAttendee;
    })(Attendee);
    RaffleJS.RegularAttendee = RegularAttendee;

    var Organizer = (function (_super) {
        __extends(Organizer, _super);
        function Organizer(name, email, favoriteSoda) {
            _super.call(this, name, email, favoriteSoda);
        }
        Organizer.prototype.getPrizeMessage = function (prize) {
            if (prize.name === 'Days of .NET ticket') {
                return 'WTF?, ' + this.name + ', you won a ' + prize.name + '! This seems suspect.';
            } else {
                return _super.prototype.getPrizeMessage.call(this, prize);
            }
        };
        return Organizer;
    })(RegularAttendee);
    RaffleJS.Organizer = Organizer;

    var RaffleService = (function () {
        function RaffleService() {
        }
        RaffleService.prototype.getPrizes = function (callback) {
            setTimeout(function () {
                callback([
                    new Prize('Free 14 day trial of Office 2013'),
                    new Prize('Pluralsight subscription'),
                    new Prize('Days of .NET ticket')
                ]);
            }, 1000);
        };

        RaffleService.prototype.getAttendees = function (callback) {
            setTimeout(function () {
                callback([
                    new Attendee('Moe', 'moe@hotmail.com'),
                    new Attendee('Larry', 'larry@geocities.com'),
                    new Attendee('Curly', 'curly@altavista.com'),
                    new RegularAttendee('Jake', 'jake@gmail.com', 'Dr. Pepper'),
                    new Organizer('Scott', 'scott@ms.com')
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
    RaffleJS.RaffleService = RaffleService;
})(RaffleJS || (RaffleJS = {}));
$(function () {
    $('#raffleButton').click(function () {
        var raffleService = new RaffleJS.RaffleService();
        raffleService.raffle();
    });
});
//# sourceMappingURL=modules.js.map
