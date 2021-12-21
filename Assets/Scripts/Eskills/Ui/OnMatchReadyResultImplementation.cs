using UnityEngine;
using UnityEngine.UI;

namespace Eskills.Ui
{
    public class OnMatchReadyResultImplementation : OnMatchReadyResult
    {
        [SerializeField] private Text roomIdText;


        public override void OnMatchReady(string session)
        {
            Debug.Log("OnMatchReadyResultImplementation" + session);
            roomIdText.text = session;
        }
    }
}