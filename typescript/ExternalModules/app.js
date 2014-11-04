///<reference path="jquery.d.ts" />
define(["require", "exports", 'RaffleService'], function(require, exports, Services) {
    $(function () {
        $('#raffleButton').click(function () {
            var raffleService = new Services.RaffleService();
            raffleService.raffle();
        });
    });
});
//# sourceMappingURL=app.js.map
