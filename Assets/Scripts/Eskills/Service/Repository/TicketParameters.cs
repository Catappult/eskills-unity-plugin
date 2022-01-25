
using System;
using System.Collections.Generic;

namespace Eskills.Service.Repository
{
    public class TicketParameters
    {
        public readonly string packageName;
        public readonly Decimal ticketPrice;
        public readonly string priceCurrency;
        public readonly string sku;
        public readonly MatchEnvironment matchEnvironment;
        public int numberOfUsers;
        public int matchMaxDuration;    


        public TicketParameters(
            string packageName,
            Decimal ticketPrice,
            string priceCurrency,
            string sku,
            MatchEnvironment matchEnvironment,
            int numberOfUsers,
            int matchMaxDuration 
        )
        {
            this.packageName = packageName;
            this.ticketPrice = ticketPrice;
            this.priceCurrency = priceCurrency;
            this.sku = sku;
            this.matchEnvironment = matchEnvironment;
            this.numberOfUsers = numberOfUsers;
            this.matchMaxDuration = matchMaxDuration;
        }
    }
}