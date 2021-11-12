using System;
using Eskills.Scripts.Service;
using UnityEngine;

namespace Eskills.Scripts.Ui
{
    public class OnMatchCreatedReceiver : MonoBehaviour, ICoroutineExecutor
    {
        [SerializeField] private OnMatchReadyResult onMatchReady;
        
        public void OnMatchCreated(string session)
        {
            Debug.Log("OnMatchCreated: " + session);
            if (onMatchReady == null)
            {
                onMatchReady = GetComponent<OnMatchReadyResult>();
                if (onMatchReady == null)
                {
                    throw new Exception("You have to attach an IOnMatchReady instance in Eskills game object");
                }
            }
            onMatchReady.OnMatchReady(session);
        }
    }
}