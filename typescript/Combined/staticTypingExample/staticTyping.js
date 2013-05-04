var raffleButton = document.getElementById('raffleButton');
raffleButton.addEventListener('click', function () {
    var numberOfAttendeesField;
    numberOfAttendeesField = document.getElementById('numberOfAttendees');
    var numberOfPrizesField;
    numberOfPrizesField = document.getElementById('numberOfPrizes');
    var numberOfAttendees = numberOfAttendeesField.value;
    var numberOfPrizes = numberOfPrizesField.value;
    var remainingAttendees = [];
    for(var i = 0; i < numberOfAttendees; i++) {
        remainingAttendees.push(i);
    }
    var results = document.getElementById('results');
    results.innerHTML = '';
    for(var i = 0; i < numberOfPrizes; i++) {
        var remainingIndex = Math.floor((Math.random() * remainingAttendees.length));
        var winner = remainingAttendees.splice(remainingIndex, 1);
        results.innerHTML += "Attendee " + (winner + 1) + " wins prize " + (i + 1) + '<br/>';
    }
});
//@ sourceMappingURL=staticTyping.js.map
