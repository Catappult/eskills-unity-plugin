using System;
using Eskills.Service;
using UnityEngine;

namespace Eskills.Ui
{
    public class OnMatchCompletedReceiver : MonoBehaviour, ICoroutineExecutor
    {
        [SerializeField] private OnMatchCompletedResult onMatchCompletedResult;
        
        public void OnMatchCompleted(string resultCode)
        {
            Debug.Log("OnMatchCompleted: " + resultCode);
            onMatchCompletedResult = GetComponent<OnMatchCompletedResult>();
            if (onMatchCompletedResult == null)
            {
                throw new Exception("You have to attach an IOnMatchCompleted instance in Eskills game object");
            }
            onMatchCompletedResult.OnMatchCompleted(resultCode);
        }
    }
}