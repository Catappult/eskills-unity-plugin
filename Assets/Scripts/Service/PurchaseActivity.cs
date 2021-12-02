using UnityEngine;

namespace Eskills.Scripts
{
    public class PurchaseActivity
    {
        private AndroidJavaClass _eskillsActivity;

        public PurchaseActivity()
        {
            _eskillsActivity = new AndroidJavaClass("com.appcoins.eskills_purchase.PurchaseActivity");
        }

        public void start(string userName, float value, string currency, string product, int timeout,
            MatchEnvironment matchEnvironment, int numberOfPlayers)
        {
            var androidJC = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            var currentActivity = androidJC.GetStatic<AndroidJavaObject>("currentActivity");

            _eskillsActivity.CallStatic("start", currentActivity, userName, value, currency, product,
                timeout, userName, matchEnvironment.ToString(), numberOfPlayers);
        }
    }
}