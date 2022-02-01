using System;
using System.Collections;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

namespace Eskills.Service.Repository
{
    public class TicketRepository
    {
        private readonly TicketResponseMapper _mapper;
        public TicketRepository(TicketResponseMapper mapper)
        {
            _mapper = mapper;
        }

        public IEnumerator CreateTicket(string ewt,TicketParameters ticketParameters, Action<TicketData> success,
            Action<EskillsError> error)
        {
            var body = new CreateTicketBody("user_id","test_user", ticketParameters);
            var room_metadata = new UTF8Encoding().GetBytes(JsonUtility.ToJson(body.room_metadata)); 
            var jsonString = JsonConvert.SerializeObject(body);
            Debug.Log(jsonString);
            byte[] jsonToSend = new UTF8Encoding().GetBytes(jsonString);
            Debug.Log("TicketRepo:"+System.Text.Encoding.Default.GetString(jsonToSend));
            
            using (var request = new UnityWebRequest("https://api.eskills.catappult.io/queue/ticket/","POST"))
            {
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Authorization", "Bearer " + ewt);
                request.uploadHandler = new UploadHandlerRaw(jsonToSend);
                request.downloadHandler = new DownloadHandlerBuffer();
                yield return request.SendWebRequest();


                if (request.isNetworkError || request.isHttpError)
                {
                    error(new EskillsError(ErrorCode.ApiCall, request.error));
                }
                else
                {
                    success(_mapper.Map(request.downloadHandler.text));
                }
            }
        }
        //Ver contexto em vez de ter dois botoes
        //Application.platform == RuntimePlatform.Android
        //Fazer login
        //GetComponent com tipo On MatchCreatedReceiver ( no service) no on succsess

        public IEnumerator GETTicketData(string ewt,string ticket_id, Action<TicketData> success,
            Action<EskillsError> error)
        {
            using (var request = UnityWebRequest.Get("https://api.eskills.catappult.io/queue/ticket/"+ticket_id))
            {
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Authorization", "Bearer " + ewt);
                yield return request.SendWebRequest();


                if (request.isNetworkError || request.isHttpError)
                {
                    error(new EskillsError(ErrorCode.ApiCall, request.error));
                }
                else
                {
                    success(_mapper.Map(request.downloadHandler.text));
                }
            }
        }
    }
}