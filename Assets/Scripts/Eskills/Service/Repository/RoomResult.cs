using System;

namespace Eskills.Service.Repository
{
    public class RoomResult
    {
        public readonly User winner;
        public readonly string winnerTx;
        public readonly Decimal winnerAmount;
        public readonly string developerTx;
        public readonly Decimal developerAmount;
        public readonly string catappultTx;
        public readonly Decimal catappultAmount;

        public RoomResult(User winner, string winnerTx, decimal winnerAmount, string developerTx,
            decimal developerAmount, string catappultTx, decimal catappultAmount)
        {
            this.winner = winner;
            this.winnerTx = winnerTx;
            this.winnerAmount = winnerAmount;
            this.developerTx = developerTx;
            this.developerAmount = developerAmount;
            this.catappultTx = catappultTx;
            this.catappultAmount = catappultAmount;
        }
    }
}