///<reference path="../jquery.d.ts" />

import Services = module('raffleService');
$(function () {
    $('#raffleButton').click(function () {
        var raffleService = new Services.RaffleService();
        raffleService.raffle();

    });  
});
