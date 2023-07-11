using Eskills.Service;
using UnityEngine;

namespace Eskills.Scripts
{
    public class EndgameActivity
    {
        private AndroidJavaClass _eskillsActivity;

        public EndgameActivity()
        {
            _eskillsActivity = new AndroidJavaClass("com.appcoins.eskills_endgame.EndgameActivity");
        }

        public void start(string session)
        {
            var androidJC = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            var currentActivity = androidJC.GetStatic<AndroidJavaObject>("currentActivity");

            _eskillsActivity.CallStatic("start", currentActivity, session);
        }
    }
}