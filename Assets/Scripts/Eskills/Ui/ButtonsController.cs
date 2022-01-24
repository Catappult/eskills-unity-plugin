using System;
using System.Collections;
using Eskills.Service.Repository;
using UnityEngine;
using UnityEngine.UI;

namespace Eskills.Ui
{
    public class ButtonsController : MonoBehaviour
    {
        [SerializeField] private EskillsService service;
        [SerializeField] private Text sessionText;
        [SerializeField] private Text scoreText;
        [SerializeField] private Dropdown playerStatus;

        private Boolean periodicUpdate = true;

        public void OnGetRoomInfoClick()
        {
            service.GetRoomInfo(sessionText.text, room => Debug.Log("ButtonsController: " + room.roomId),
                error => Debug.Log("ButtonsController: " + error.Message));
        }

        public void OnSetScoreClick()
        {
            var status = (SetScoreBody.Status) Enum.Parse(typeof(SetScoreBody.Status),
                playerStatus.options[playerStatus.value].text);

            service.SetScore(sessionText.text, status, int.Parse(scoreText.text),
                room => Debug.Log("ButtonsController: " + room.roomId),
                error => Debug.Log("ButtonsController: " + error.Message));
        }
	    public void OnGetPeriodicUpdates()
        {
            var coroutine = GetPeriodicRoomInfoUpdates(sessionText.text,
                room => Debug.Log("Periodic Score Update:"+room.roomId),
                error => Debug.Log(error.Message));
            StartCoroutine(coroutine);
        }

        public void OnWaitForAllPlayersToFinish(){
            var coroutine = GetPeriodicRoomInfoUpdates(
                sessionText.text,
                room => {
                    if(room.status == RoomStatus.COMPLETED){
                        cancelPeriodicUpdate();
                        Debug.Log(room.roomResult.winner.userName+ " Won!");
                    }
                    else {
                        Debug.Log("Waiting For All Players to Finish");
                    }
                },
                error => Debug.Log(error.Message)
            );
            StartCoroutine(coroutine);
        }

        private IEnumerator GetPeriodicRoomInfoUpdates(string session, Action<RoomData> success, Action<EskillsError> error)
        {
            while (periodicUpdate)
            {
                service.GetRoomInfo(session,success,error);
                yield return new WaitForSeconds(5.0f);
            }
        }
        public void cancelPeriodicUpdate()
        {
            periodicUpdate = false;
        }
    }
}
