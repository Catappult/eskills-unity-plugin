namespace Eskills.Service.Repository
{
    public class User
    {
        public string userId;
        public double score;
        public string userName;

        public User(string userId, double score, string userName)
        {
            this.userId = userId;
            this.score = score;
            this.userName = userName;
        }
    }
}