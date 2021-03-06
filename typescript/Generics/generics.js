﻿///<reference path="jquery.d.ts" />
//declare var $: any;
var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
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

var RegularAttendee = (function (_super) {
    __extends(RegularAttendee, _super);
    function RegularAttendee(name, email, favoriteSoda) {
        if (typeof favoriteSoda === "undefined") { favoriteSoda = 'bud light'; }
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

var Organizer = (function (_super) {
    __extends(Organizer, _super);
    function Organizer(name, email, favoriteSoda) {
        _super.call(this, name, email, favoriteSoda);
    }
    Organizer.prototype.getPrizeMessage = function (prize) {
        if (prize.name === 'Days of .NET ticket') {
            return 'WTF?, ' + this.name + ', You won a ' + prize.name + '! This seems suspect.';
        } else {
            return _super.prototype.getPrizeMessage.call(this, prize);
        }
    };
    return Organizer;
})(RegularAttendee);

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
        new Attendee('Curly', 'curly@altavista.com'),
        new RegularAttendee('Jake', 'jake@gmail.com', 'Dr. Pepper'),
        new Organizer('Scott', 'scott@ms.com')
    ];
}

function selectRandomItemAndRemove(items) {
    var selectedIndex = Math.floor((Math.random() * items.length));
    var selectedItem = items.splice(selectedIndex, 1)[0];
    return selectedItem;
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

            //var winner = selectRandomItemAndRemove<Attendee>(attendees);
            results.append(winner.getPrizeMessage(prizes[i]) + '<br/>');
        }
    });
});
//# sourceMappingURL=generics.js.map
