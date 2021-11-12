using System;
using Eskills.Scripts.Ui;
using UnityEngine;
using UnityEngine.UI;

namespace UI
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