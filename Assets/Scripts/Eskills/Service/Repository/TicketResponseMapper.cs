using System;
using System.Collections.Generic;
using SimpleJSON;

namespace Eskills.Service.Repository
{
    public class TicketResponseMapper
    {
        public TicketData Map(string json)
        {
            JSONNode parsedData = JSON.Parse(json);
            Enum.TryParse(parsedData["ticket_status"],out TicketStatus ticketStatus);
            string ticketId = parsedData["ticket_id"];
            string walletAddress = parsedData["wallet_address"];
            string packageName = parsedData["package_name"];
            string userId = parsedData["user_id"];
            string userName = parsedData["user_name"];
            Decimal.TryParse(parsedData["ticket_price"], out Decimal ticketPrice);
            string priceCurrency = parsedData["price_currency"];
            string productToken = parsedData["product_token"];
            string roomId = parsedData["room_id"];
            return new TicketData(ticketId, ticketStatus,  walletAddress,packageName, userId, userName, priceCurrency, productToken, roomId);
        }
    }
}