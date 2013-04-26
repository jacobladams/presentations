$(function () {
    $('#raffleButton').click(function () {
        var raffleService = new RaffleJS.Services.RaffleService();
        raffleService.raffle();
    });
});
