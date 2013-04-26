var __extends = this.__extends || function (d, b) {
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
var MockAttendee = (function () {
    function MockAttendee() { }
    MockAttendee.prototype.getPrizeMessage = function (prize) {
        return 'Test Message with prize named: ' + prize.name + ' was passed in';
    };
    return MockAttendee;
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
    function RegularAttendee(name, email, favoriteBeer) {
        _super.call(this, name, email);
        this.name = name;
        this.favoriteBeer = favoriteBeer;
    }
    RegularAttendee.prototype.getPrizeMessage = function (prize) {
        if(prize.name === 'Platic Water Bottle with Conference Logo') {
            return 'Tough luck, ' + this.name + '! You won another ' + prize.name;
        } else {
            return _super.prototype.getPrizeMessage.call(this, prize);
        }
    };
    return RegularAttendee;
})(Attendee);
var Organizer = (function (_super) {
    __extends(Organizer, _super);
    function Organizer(name, email, favoriteBeer) {
        _super.call(this, name, email, favoriteBeer);
        this.name = name;
    }
    Organizer.prototype.getPrizeMessage = function (prize) {
        if(prize.name === 'Golden xBox 720 Ultimate RT Pro 8 Series - 64 bit') {
            return 'WTF?, ' + this.name + ', you won a ' + prize.name + '! This seems suspect.';
        } else {
            return _super.prototype.getPrizeMessage.call(this, prize);
        }
    };
    return Organizer;
})(RegularAttendee);
function getPrizes() {
    return [
        new Prize('Platic Water Bottle with Conference Logo'), 
        new Prize('Signed Anders Hejlsberg Photo'), 
        new Prize('Golden xBox 720 Ultimate RT Pro 8 Series - 64 bit')
    ];
}
function getAttendees() {
    return [
        new Attendee('Moe', 'moe@hotmail.com'), 
        new Attendee('Larry', 'larry@geocities.com'), 
        new Attendee('Curly', 'curly@altavista.com'), 
        new RegularAttendee('Jake', 'jake@gmail.com', 'Boulevard Tank 7'), 
        new Organizer('Nick', 'nick@github.com'), 
        new MockAttendee()
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
        for(var i = 0; i < numberOfPrizes; i++) {
            var winningIndex = Math.floor((Math.random() * attendees.length));
            var winner = attendees.splice(winningIndex, 1)[0];
            results.append(winner.getPrizeMessage(prizes[i]) + '<br/>');
        }
    });
});
