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
exports.Prize = Prize;
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
exports.Attendee = Attendee;
var RegularAttendee = (function (_super) {
    __extends(RegularAttendee, _super);
    function RegularAttendee(name, email, favoriteBeer) {
        _super.call(this, name, email);
        this.name = name;
        this.favoriteBeer = favoriteBeer;
    }
    RegularAttendee.prototype.getPrizeMessage = function (prize) {
        if(prize.name === 'Water Bottle with Conference Logo') {
            return 'Tough luck, ' + this.name + '! You won another ' + prize.name;
        } else {
            return _super.prototype.getPrizeMessage.call(this, prize);
        }
    };
    return RegularAttendee;
})(Attendee);
exports.RegularAttendee = RegularAttendee;
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
exports.Organizer = Organizer;
//@ sourceMappingURL=models.js.map
