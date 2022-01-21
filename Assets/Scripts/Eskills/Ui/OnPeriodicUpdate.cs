using System;
using System.Collections;
using Eskills.Service.Repository;
using UnityEngine;
using UnityEngine.UI;

namespace Eskills.Ui
{
    [Serializable]
    public abstract class OnPeriodicUpdate : MonoBehaviour
    {
        public abstract void OnSuccess(RoomData room);

        public abstract void OnError(EskillsError error);
    }
}