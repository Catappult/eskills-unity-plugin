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
            List<User> usersList = MapUsers(parsedData["users"]);
            Enum.TryParse(parsedData["status"], out RoomStatus roomStatus);

            return new RoomData(roomId, currentUser, usersList, roomStatus);
        }

        private List<User> MapUsers(JSONNode usersNode)
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
            return new User(userId, score, userName);
        }
    }
}