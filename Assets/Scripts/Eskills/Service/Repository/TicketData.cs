using System;
using System.Collections.Generic;

namespace Eskills.Service.Repository
{
    public class TicketData
    {
        public readonly string ticketId;
        public readonly TicketStatus ticketStatus;
        public readonly string walletAddress;
        public readonly string packageName;
        public readonly string userId;
        public readonly string userName;
        public readonly Decimal ticketPrice;
        public readonly string priceCurrency;
        public readonly string productToken;
        public readonly string roomId;


        public TicketData(
            string ticketId, 
            TicketStatus ticketStatus, 
            string walletAddress, 
            string packageName, 
            string userId,
            string userName,
            string priceCurrency,
            string productToken,
            string roomId
        )
        {
            this.ticketId = ticketId;
            this.ticketStatus = ticketStatus;
            this.walletAddress = walletAddress;
            this.packageName = packageName;
            this.userId = userId;
            this.userName = userName;
            this.priceCurrency = priceCurrency;
            this.productToken = productToken;
            this.roomId = roomId;
        }
    }
}