using System;
using System.Collections;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

namespace Eskills.Service.Repository
{
    public class RoomRepository
    {
        private readonly RoomResponseMapper _mapper;

        public RoomRepository(RoomResponseMapper mapper)
        {
            _mapper = mapper;
        }

        public IEnumerator GETRoomData(string session, Action<RoomData> success,
            Action<EskillsError> error)
        {
            using (var request = UnityWebRequest.Get("https://api.eskills.catappult.io/room/"))
            {
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Authorization", "Bearer " + session);
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

        public IEnumerator SetScore(string session, SetScoreBody.Status status, int score,
            [CanBeNull] Action<RoomData> success, [CanBeNull] Action<EskillsError> error)
        {
            using (var request = new UnityWebRequest("https://api.eskills.catappult.io/room/", "PATCH"))
            {
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Authorization", "Bearer " + session);
                var body = new SetScoreBody(status, score);

                byte[] jsonToSend = new UTF8Encoding().GetBytes(JsonUtility.ToJson(body));
                string authorizationHeader = "Bearer " + session;
                request.uploadHandler = new UploadHandlerRaw(jsonToSend);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Authorization", authorizationHeader);

                yield return request.SendWebRequest();


                if (request.isNetworkError || request.isHttpError)
                {
                    error?.Invoke(new EskillsError(ErrorCode.ApiCall, request.error));
                }
                else
                {
                    success?.Invoke(_mapper.Map(request.downloadHandler.text));
                }
            }
        }

        public IEnumerator Login(string ewt, string ticketId,string roomId, Action<string> success,
            Action<EskillsError> error)
        {
            var body = new LoginBody {
                ticket_id = ticketId,
                room_id = roomId
            };
            var jsonString = JsonConvert.SerializeObject(body);
            Debug.Log(jsonString);
            byte[] jsonToSend = new UTF8Encoding().GetBytes(jsonString);
            Debug.Log("TicketRepo:"+System.Text.Encoding.Default.GetString(jsonToSend));
            
            using (var request = new UnityWebRequest("https://api.eskills.catappult.io/room/authorization/login", "POST"))
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
                    success(_mapper.MapToken(request.downloadHandler.text));
                }
            }
        }

        public class LoginBody
        {
            public string room_id { get; set; }
            public string ticket_id { get; set; }
        }
        
    }
}