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
        [SerializeField] private Action<RoomData> success;
        [SerializeField] private Action<EskillsError> error;

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
            var coroutine = GetPeriodicRoomInfoUpdates(sessionText.text,success,error);
            StartCoroutine(coroutine);
        }

        private IEnumerator GetPeriodicRoomInfoUpdates(string session, Action<RoomData> success, Action<EskillsError> error)
        {
            while (periodicUpdate)
            {
                service.GetRoomInfo(session,success,error);
                Debug.Log("PERIODIC_SCORE_UPDATE");
                yield return new WaitForSeconds(5.0f);
            }
        }
        private void Success(RoomData room){
            Debug.Log(room.status);
            Debug.Log(room.roomId);
            Debug.Log(room.players);
        }
        private void Error(EskillsError error){
            Debug.Log(error);
        }
        public void cancelPeriodicUpdate()
        {
            periodicUpdate = false;
        }
    }
}
