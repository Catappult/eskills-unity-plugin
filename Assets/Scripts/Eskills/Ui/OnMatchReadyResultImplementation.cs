using UnityEngine;
using UnityEngine.UI;

namespace Eskills.Ui
{
    public class OnMatchReadyResultImplementation : OnMatchReadyResult
    {
        [SerializeField] private Text roomIdText;


        public override void OnMatchReady(string matchId)
        {
            Debug.Log("OnMatchReadyResultImplementation" + matchId);
            roomIdText.text = matchId;
        }
    }
}