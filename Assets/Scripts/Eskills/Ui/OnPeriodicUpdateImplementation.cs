using System;
using System.Collections;
using Eskills.Service.Repository;
using UnityEngine;
using UnityEngine.UI;

namespace Eskills.Ui
{
    [Serializable]
    public class OnPeriodicUpdateImplementation : OnPeriodicUpdate
    {
        public override void OnSuccess(RoomData room)
        {
            Debug.Log(room.roomId);
        }

        public override void OnError(EskillsError error)
        {
            Debug.Log(error);
        }
    }
}