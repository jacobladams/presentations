///<reference path="jquery.d.ts" />
var Prize = (function () {
    function Prize(name) {
        this.name = name;
    }
    return Prize;
})();

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

function getPrizes() {
    return [
        new Prize('Free 14 day trial of Office 2013'),
        new Prize('Pluralsight subscription'),
        new Prize('Days of .NET ticket')
    ];
}

function getAttendees() {
    return [
        new Attendee('Moe', 'moe@hotmail.com'),
        new Attendee('Larry', 'larry@geocities.com'),
        new Attendee('Curly', 'curly@altavista.com')
    ];
}

$(function () {
    $('#raffleButton').click(function () {
        var attendees = getAttendees();

        var prizes = getPrizes();

        var numberOfAttendees = attendees.length;

        var numberOfPrizes = prizes.length;

        var results = $('#results');
        results.html('');
        for (var i = 0; i < numberOfPrizes; i++) {
            var winningIndex = Math.floor((Math.random() * attendees.length));
            var winner = attendees.splice(winningIndex, 1)[0];

            results.append(winner.getPrizeMessage(prizes[i]) + '<br/>');
        }
    });
});
//# sourceMappingURL=classes.js.map
