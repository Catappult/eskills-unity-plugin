using System.Collections.Generic;

namespace Eskills.Service.Repository
{
    public class User
    {
        public string walletAddress;
        public string userId;
        public string userName;
        public string ticketId;
        public Dictionary<string, string> roomMetadata;
        public PlayerStatus status;
        public double score;

        public User(string userId, double score, string userName, string walletAddress, string ticketId,
            Dictionary<string, string> roomMetadata, PlayerStatus status)
        {
            this.userId = userId;
            this.score = score;
            this.userName = userName;
            this.walletAddress = walletAddress;
            this.ticketId = ticketId;
            this.roomMetadata = roomMetadata;
            this.status = status;
        }
    }

    public enum PlayerStatus
    {
        PLAYING,
        COMPLETED,
        TIME_UP
    }
}