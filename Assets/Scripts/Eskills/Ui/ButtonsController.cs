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
        private bool periodicUpdate = false;
        private string ewt="eyJ0eXAiOiJFV1QifQ.eyJpc3MiOiIweDNiRWJmNmE1Mzg2MTgwMDZCYzEwNUY2OTY0MTlkRjNmYkQ2N2VCMkUiLCJleHAiOjEwMjU3MDk2OTI1fQ.0eca45625db7de7debc43b4094340709fbbcba9ab4cac5ce2990aafd3d14fcee03366741bea1698cc60d2838058b43332322ca1dfab60bb673ef0715d68e9a0101";

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

        public void OnCreateTicketClick()
        {
            Debug.Log("HERE!!");
            service.CreateTicket(ewt,
            ticket => {
                Debug.Log("ButtonsController:" + ticket.ticketId);
                OnWaitForMatch(ticket.ticketId);
            },
            error => Debug.Log("ButtonsController:"+ error.Message));
        }
        
        private void OnWaitForMatch(string ticketId){
            var coroutine = GetTicketUpdate(ewt, ticketId, 
            ticket =>{
                Debug.Log("Waiting for players to match");
                Debug.Log("Ticket Status" + ticket.ticketStatus);
                if(ticket.ticketStatus == TicketStatus.COMPLETED){
                    Debug.Log("GameStarted");
                    cancelPeriodicUpdate();
                }
            }, 
            error => Debug.Log("Something went wrong"));
            StartCoroutine(coroutine);
        }

        private IEnumerator GetTicketUpdate(string ewt,string ticketId, Action<TicketData> success, Action<EskillsError> error)
        {
            periodicUpdate = true;
            while (periodicUpdate)
            {
                service.GetTicket(ewt,ticketId,success,error);
                yield return new WaitForSeconds(5.0f);
            }
        }
        public void cancelPeriodicUpdate()
        {
            periodicUpdate = false;
        }
    }
}