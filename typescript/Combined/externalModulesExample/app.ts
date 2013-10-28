///<reference path="../jquery.d.ts" />

import Services = require('raffleService');
$(function () {
    $('#raffleButton').click(function () {
        var raffleService = new Services.RaffleService();
        raffleService.raffle();

    });  
});
