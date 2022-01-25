using System;

namespace Eskills.Service.Repository
{
    [Serializable]
    public class CreateTicketBody
    {
        public string userId;
        public string userName;
        public string packageName;
        public string sku;
        public string matchEnvironment;
        public int numberOfUsers;
        public Decimal ticketPrice;
        public int matchMaxDuration;

        public CreateTicketBody(string userId, string userName,TicketParameters ticketParameters)
        {
            switch (ticketParameters.matchEnvironment)
            {
                case MatchEnvironment.SANDBOX:
                {
                    this.matchEnvironment = "SANDBOX";
                    break;
                }
                case MatchEnvironment.LIVE:
                {
                    this.matchEnvironment= "LIVE";
                    break;
                }
            }
            this.userId = userId;
            this.userName = userName;
            this.packageName = ticketParameters.packageName;
            this.sku = ticketParameters.sku;
            this.numberOfUsers = ticketParameters.numberOfUsers;
            this.ticketPrice = ticketParameters.ticketPrice;
            this.matchMaxDuration = ticketParameters.matchMaxDuration;
        }
    }
}