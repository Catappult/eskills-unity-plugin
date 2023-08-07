using UnityEngine;
using UnityEngine.UI;

namespace Eskills.Ui
{
    public class OnMatchCompletedResultImplementation : OnMatchCompletedResult
    {
        public override void OnMatchCompleted(string resultCode)
        {
            Debug.Log("OnMatchCompletedResultImplementation: " + resultCode);
        }
    }
}