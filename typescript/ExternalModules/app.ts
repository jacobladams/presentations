///<reference path="jquery.d.ts" />

import Services = require('RaffleService');
$(function () {
    $('#raffleButton').click(function () {
        var raffleService = new Services.RaffleService();
        raffleService.raffle();

    });  
});
