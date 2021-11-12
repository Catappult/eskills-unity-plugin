using System;

namespace Eskills.Scripts.Service.Repository
{
    [Serializable]
    public class SetScoreBody
    {
        public double score;
        public string status;

        public SetScoreBody(Status status, double score)
        {
            switch (status)
            {
                case Status.PLAYING:
                {
                    this.status = "PLAYING";
                    break;
                }
                case Status.COMPLETED:
                {
                    this.status = "COMPLETED";
                    break;
                }
            }

            this.score = score;
        }


        public enum Status
        {
            PLAYING,
            COMPLETED
        }
    }
}