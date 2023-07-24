using System;
using UnityEngine;

namespace Eskills.Ui
{
    [Serializable]
    public abstract class OnMatchCompletedResult : MonoBehaviour
    {
        public abstract void OnMatchCompleted(string resultCode);
    }
}