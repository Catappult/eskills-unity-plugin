using System;
using Eskills.Scripts;
using Eskills.Scripts.Service.Repository;
using UnityEngine;
using UnityEngine.UI;

namespace UI
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
    }
}