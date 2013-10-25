///<reference path="jquery.d.ts" />
///<reference path="Models.ts" />
///<reference path="RaffleService.ts" />
$(function () {
    $('#raffleButton').click(function () {
        var raffleService = new RaffleJS.Services.RaffleService();
        raffleService.raffle();
    });
});
//# sourceMappingURL=app.js.map
