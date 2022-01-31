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
            service.GetPeriodicUpdate(sessionText.text,
                room => {
                    Debug.Log("Periodic Score Update:"+room.roomId);
                    if(room.currentUser.status == PlayerStatus.COMPLETED){
                        service.StopPeriodicUpdate();
                        OnWaitForPlayersToFinnish();
                    }
                },
                error =>  Debug.Log(error.Message)
            );

        }

        public void OnWaitForPlayersToFinnish()
        {
            service.WaitForPlayersToFinnish(sessionText.text, 
                room=> Debug.Log(room.roomResult.winner.userName + "Won!"), 
                error => Debug.Log(error));
        }
    }
}
