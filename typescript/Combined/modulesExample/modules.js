var __extends = this.__extends || function (d, b) {
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var JakesRaffleService;
(function (JakesRaffleService) {
    var Prize = (function () {
        function Prize(name) {
            this.name = name;
        }
        return Prize;
    })();
    JakesRaffleService.Prize = Prize;    
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
    JakesRaffleService.Attendee = Attendee;    
    var RegularAttendee = (function (_super) {
        __extends(RegularAttendee, _super);
        function RegularAttendee(name, email, favoriteBeer) {
                _super.call(this, name, email);
            this.name = name;
            this.favoriteBeer = favoriteBeer;
        }
        RegularAttendee.prototype.getPrizeMessage = function (prize) {
            if(prize.name === 'Succeeding with Agile book') {
                return 'Tough luck, ' + this.name + '! You won another copy of ' + prize.name;
            } else {
                return _super.prototype.getPrizeMessage.call(this, prize);
            }
        };
        return RegularAttendee;
    })(Attendee);
    JakesRaffleService.RegularAttendee = RegularAttendee;    
    var Organizer = (function (_super) {
        __extends(Organizer, _super);
        function Organizer(name, email, favoriteBeer) {
                _super.call(this, name, email, favoriteBeer);
            this.name = name;
        }
        Organizer.prototype.getPrizeMessage = function (prize) {
            if(prize.name === 'KCDC Ticket') {
                return 'WTF?, ' + this.name + ', you won a ' + prize.name + '! This seems suspect.';
            } else {
                return _super.prototype.getPrizeMessage.call(this, prize);
            }
        };
        return Organizer;
    })(RegularAttendee);
    JakesRaffleService.Organizer = Organizer;    
    var RaffleService = (function () {
        function RaffleService() { }
        RaffleService.prototype.getPrizes = function (callback) {
            setTimeout(function () {
                callback([
                    new Prize('Succeeding with Agile book'), 
                    new Prize('Pluralsight Subscription'), 
                    new Prize('KCDC Ticket')
                ]);
            }, 1000);
        };
        RaffleService.prototype.getAttendees = function (callback) {
            setTimeout(function () {
                callback([
                    new Attendee('Moe', 'moe@hotmail.com'), 
                    new Attendee('Larry', 'larry@geocities.com'), 
                    new Attendee('Curly', 'curly@altavista.com'), 
                    new RegularAttendee('Jake', 'jake@gmail.com', 'Left Hand Haystack'), 
                    new Organizer('Nick', 'nick@github.com'), 
                    
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
                    for(var i = 0; i < numberOfPrizes; i++) {
                        var winningIndex = Math.floor((Math.random() * _this._attendees.length));
                        var winner = _this._attendees.splice(winningIndex, 1)[0];
                        results.append(winner.getPrizeMessage(_this._prizes[i]) + '<br/>');
                    }
                });
            });
        };
        return RaffleService;
    })();
    JakesRaffleService.RaffleService = RaffleService;    
})(JakesRaffleService || (JakesRaffleService = {}));
$(function () {
    $('#raffleButton').click(function () {
        var raffleService = new JakesRaffleService.RaffleService();
        raffleService.raffle();
    });
});
