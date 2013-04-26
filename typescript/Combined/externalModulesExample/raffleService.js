define(["require", "exports", 'models'], function(require, exports, __Models__) {
    var Models = __Models__;

    var RaffleService = (function () {
        function RaffleService() { }
        RaffleService.prototype.getPrizes = function (callback) {
            setTimeout(function () {
                callback([
                    new Models.Prize('Succeeding with Agile book'), 
                    new Models.Prize('Pluralsight Subscription'), 
                    new Models.Prize('KCDC Ticket')
                ]);
            }, 1000);
        };
        RaffleService.prototype.getAttendees = function (callback) {
            setTimeout(function () {
                callback([
                    new Models.Attendee('Moe', 'moe@hotmail.com'), 
                    new Models.Attendee('Larry', 'larry@geocities.com'), 
                    new Models.Attendee('Curly', 'curly@altavista.com'), 
                    new Models.RegularAttendee('Jake', 'jake@gmail.com', 'Left Hand Haystack'), 
                    new Models.Organizer('Nick', 'nick@github.com'), 
                    
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
    exports.RaffleService = RaffleService;    
})
