///<reference path="jquery.d.ts" />

import Services = module('RaffleService');
$(function () {
    $('#raffleButton').click(function () {
        var raffleService = new Services.RaffleService();
        raffleService.raffle();

    });  
});
