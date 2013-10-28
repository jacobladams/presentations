///<reference path = "..\jquery.d.ts" / >
//declare var $: any;
$(function () {
    $('#raffleButton').click(function () {
        var numberOfAttendeesField = $('#numberOfAttendees');

        var numberOfPrizesField = $('#numberOfPrizes');

        var numberOfAttendees = numberOfAttendeesField.val();

        var numberOfPrizes = numberOfPrizesField.val();

        var remainingAttendees = [];

        for (var i = 0; i < numberOfAttendees; i++) {
            remainingAttendees.push(i);
        }

        var results = $('#results');
        results.html('');
        for (var i = 0; i < numberOfPrizes; i++) {
            var remainingIndex = Math.floor((Math.random() * remainingAttendees.length));
            var winner = remainingAttendees.splice(remainingIndex, 1)[0];

            results.append("Attendee " + (winner + 1) + " wins prize " + (i + 1) + '<br/>');
        }

    });
});