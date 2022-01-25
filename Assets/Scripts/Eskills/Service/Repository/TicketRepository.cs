using System;
using System.Collections;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;

namespace Eskills.Service.Repository
{
    public class TicketRepository
    {
        private readonly TicketResponseMapper _mapper;
        private readonly TicketParameters _ticketParameters;
        public TicketRepository(TicketResponseMapper mapper)
        {
            _mapper = mapper;
            _ticketParameters = new TicketParameters(
                "com.appcoins.unitytest",
                1,
                "USD",
                "1v1",
                MatchEnvironment.SANDBOX,
                1,
                600
            );
        }

        public IEnumerator CreateTicket(string ewt, Action<TicketData> success,
            Action<EskillsError> error)
        {
            var body = new CreateTicketBody("user_id","test_user",_ticketParameters);
            byte[] jsonToSend = new UTF8Encoding().GetBytes(JsonUtility.ToJson(body));

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