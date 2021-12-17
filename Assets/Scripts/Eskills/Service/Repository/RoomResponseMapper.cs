using System;
using System.Collections.Generic;
using SimpleJSON;

namespace Eskills.Service.Repository
{
    public class RoomResponseMapper
    {
        public RoomData Map(string json)
        {
            JSONNode parsedData = JSON.Parse(json);
            string roomId = parsedData["room_id"];
            User currentUser = mapUser(parsedData["current_user"]);
            List<User> usersList = mapUsers(parsedData["users"]);
            Enum.TryParse(parsedData["status"], out RoomStatus roomStatus);
            var roomResult = mapRoomResult(parsedData["room_result"]);
            string packageName = parsedData["package_name"];
            Decimal.TryParse(parsedData["usd_stake"], out Decimal usdStake);
            return new RoomData(roomId, currentUser, usersList, roomStatus, roomResult, packageName, usdStake);
        }

        private RoomResult mapRoomResult(JSONNode roomResultNode)
        {
            Decimal.TryParse(roomResultNode["winner_amount"], out Decimal winnerAmount);
            Decimal.TryParse(roomResultNode["developer_amount"], out Decimal developerAmount);
            Decimal.TryParse(roomResultNode["catappult_amount"], out Decimal catappultAmount);
            return new RoomResult(mapUser(roomResultNode["winner"]), roomResultNode["winner_tx"],
                winnerAmount, roomResultNode["developer_tx"], developerAmount, roomResultNode["catappult_tx"],
                catappultAmount);
        }

        private List<User> mapUsers(JSONNode usersNode)
        {
            int numberOfUsers = usersNode.Count;
            List<User> users = new List<User>(numberOfUsers);

            for (int i = 0; i < numberOfUsers; i++)
            {
                var user = mapUser(usersNode[i]);
                if (user != null)
                {
                    users.Add(user);
                }
            }

            return users;
        }

        private User mapUser(JSONNode playerNode)
        {
            if (playerNode == null)
            {
                return null;
            }

            var userId = playerNode["user_id"];
            var score = playerNode["score"];
            var userName = playerNode["user_name"];
            var walletAddress = playerNode["wallet_address"];
            var ticketId = playerNode["ticket_id"];
            Enum.TryParse(playerNode["status"], out PlayerStatus playerStatus);

            var roomMetadata = new Dictionary<string, string>();

            foreach (var keyValuePair in playerNode["room_metadata"].Linq)
            {
                roomMetadata[keyValuePair.Key] = keyValuePair.Value;
            }

            return new User(userId, score, userName, walletAddress, ticketId,
                roomMetadata, playerStatus);
        }
    }
}