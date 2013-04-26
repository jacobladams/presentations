define(["require", "exports", 'raffleService'], function(require, exports, __Services__) {
    var Services = __Services__;

    $(function () {
        $('#raffleButton').click(function () {
            var raffleService = new Services.RaffleService();
            raffleService.raffle();
        });
    });
})
