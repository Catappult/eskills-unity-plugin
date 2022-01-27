using System;
using System.Collections.Generic;

namespace Eskills.Service.Repository
{
    [Serializable]
    public class CreateTicketBody
    {
        public string user_id;
        public string user_name;
        public string package_name;
        public string sku;
        public string match_environment;
        public int number_of_users;
        public Decimal ticket_price;
        public int match_max_duration;
        public Dictionary<string,string> room_metadata;

        public CreateTicketBody(string userId, string userName,TicketParameters ticketParameters)
        {
            switch (ticketParameters.matchEnvironment)
            {
                case MatchEnvironment.SANDBOX:
                {
                    this.match_environment = "SANDBOX";
                    break;
                }
                case MatchEnvironment.LIVE:
                {
                    this.match_environment= "LIVE";
                    break;
                }
            }
            this.user_id = userId;
            this.user_name = userName;
            this.package_name = ticketParameters.packageName;
            this.sku = ticketParameters.sku;
            this.number_of_users = ticketParameters.numberOfUsers;
            this.ticket_price = ticketParameters.ticketPrice;
            this.match_max_duration = ticketParameters.matchMaxDuration;
            this.room_metadata = new Dictionary < string, string > ();
            this.room_metadata.Add("additionallProp1","string");
        }

        public string getRoomMetadata()
        {
            return "room_metadata:{}";
        }
    }
}