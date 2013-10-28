///<reference path="../jquery.d.ts" />
///<reference path="models.ts" />
///<reference path="raffleService.ts" />
$(function () {
    $('#raffleButton').click(function () {
        var raffleService = new RaffleJS.Services.RaffleService();
        raffleService.raffle();
    });
});
//# sourceMappingURL=app.js.map
