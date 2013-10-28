var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
define(["require", "exports"], function(require, exports) {
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
    exports.RegularAttendee = RegularAttendee;

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
    exports.Organizer = Organizer;
});
//# sourceMappingURL=models.js.map
