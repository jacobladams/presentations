var RaffleJS;
(function (RaffleJS) {
    (function (Services) {
        var RaffleService = (function () {
            function RaffleService() { }
            RaffleService.prototype.getPrizes = function (callback) {
                setTimeout(function () {
                    callback([
                        new RaffleJS.Models.Prize('Succeeding with Agile book'), 
                        new RaffleJS.Models.Prize('Pluralsight Subscription'), 
                        new RaffleJS.Models.Prize('KCDC Ticket')
                    ]);
                }, 1000);
            };
            RaffleService.prototype.getAttendees = function (callback) {
                setTimeout(function () {
                    callback([
                        new RaffleJS.Models.Attendee('Moe', 'moe@hotmail.com'), 
                        new RaffleJS.Models.Attendee('Larry', 'larry@geocities.com'), 
                        new RaffleJS.Models.Attendee('Curly', 'curly@altavista.com'), 
                        new RaffleJS.Models.RegularAttendee('Jake', 'jake@gmail.com', 'Left Hand Haystack'), 
                        new RaffleJS.Models.Organizer('Nick', 'nick@github.com'), 
                        
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
        Services.RaffleService = RaffleService;        
    })(RaffleJS.Services || (RaffleJS.Services = {}));
    var Services = RaffleJS.Services;
})(RaffleJS || (RaffleJS = {}));
//@ sourceMappingURL=RaffleService.js.map