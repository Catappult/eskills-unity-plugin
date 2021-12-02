using System.Collections.Generic;

namespace Eskills.Service.Repository
{
    public class RoomData
    {
        public string roomId;
        public User currentUser;
        public List<User> players;
        public RoomStatus status;

        public RoomData(string roomId, User currentUser, List<User> players, RoomStatus status)
        {
            this.roomId = roomId;
            this.currentUser = currentUser;
            this.players = players;
            this.status = status;
        }
    }
}