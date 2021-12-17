using System;
using System.Collections.Generic;

namespace Eskills.Service.Repository
{
    public class RoomData
    {
        public readonly string roomId;
        public readonly User currentUser;
        public readonly List<User> players;
        public readonly RoomStatus status;
        public readonly RoomResult roomResult;
        public readonly string packageName;
        public readonly Decimal usdTotalStake;


        public RoomData(string roomId, User currentUser, List<User> players, RoomStatus status, RoomResult roomResult,
            string packageName, decimal usdTotalStake)
        {
            this.roomId = roomId;
            this.currentUser = currentUser;
            this.players = players;
            this.status = status;
            this.roomResult = roomResult;
            this.packageName = packageName;
            this.usdTotalStake = usdTotalStake;
        }
    }
}