using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Eskills.Service.Repository
{
    public class TicketRepository
    {
        private readonly string _privateKey;
        private readonly ICoroutineExecutor _executor;

        public TicketRepository(string privateKey, ICoroutineExecutor executor)
        {
            _privateKey = privateKey;
            _executor = executor;
        }


        private IEnumerator WaitForRoomCreation(string ticketId, Action<string, string> onRoomCreated)
        {
            while (true)
            {
                var request = UnityWebRequest.Get($"https://api.eskills.catappult.io/queue/ticket/{ticketId}");
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Authorization", "Bearer " + _privateKey);

                yield return request.SendWebRequest();
                if (request.result == UnityWebRequest.Result.Success)
                {
                    string json = request.downloadHandler.text;
                    TicketResponse ticketResponse = JsonUtility.FromJson<TicketResponse>(json);
                    if (!string.IsNullOrEmpty(ticketResponse.room_id))
                    {
                        Debug.Log($"RoomId: {ticketResponse.room_id}");
                        onRoomCreated(ticketResponse.ticket_id, ticketResponse.room_id);
                        break;
                    }
                }
                else
                {
                    Debug.LogError(request.downloadHandler.text);
                }

                yield return new WaitForSeconds(1);
            }
        }

        private IEnumerator GetTicketInfo(string ticketId, Action<TicketResponse> onTicketResponse)
        {
            var request = UnityWebRequest.Get($"https://api.eskills.catappult.io/queue/ticket/{ticketId}");
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + _privateKey);

            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = request.downloadHandler.text;
                TicketResponse ticketResponse = JsonUtility.FromJson<TicketResponse>(json);
                onTicketResponse(ticketResponse);
            }
            else
            {
                Debug.LogError(request.downloadHandler.text);
            }
        }

        public IEnumerator CreateTicket(string userName, float value, string currency, string product, int timeout,
            MatchEnvironment matchEnvironment, int numberOfPlayers, Action<string, string> onRoomcreated)
        {
            var ticketRequestBody = new TicketRequestBody
            {
                user_name = userName,
                price = value,
                price_currency = currency,
                sku = product,
                match_environment = matchEnvironment.ToString(),
                number_of_users = numberOfPlayers,
                match_max_duration = timeout,
                package_name = "com.appcoins.eskills2048",
                user_id = userName.ToLower(),
                wallet_address = "0xe3afc21dcd87ab5e3aeda543e63883b689e6f518",
            };

            var request = new UnityWebRequest("https://api.eskills.catappult.io/queue/ticket/", "POST");
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + _privateKey);
            string jsonRequestBody = JsonUtility.ToJson(ticketRequestBody);
            jsonRequestBody = jsonRequestBody.Insert(1, "\"room_metadata\": {},");

            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonRequestBody);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = request.downloadHandler.text;
                TicketResponse ticketResponse = JsonUtility.FromJson<TicketResponse>(json);
                Debug.Log(ticketResponse.ticket_id);
                yield return _executor.StartCoroutine(WaitForRoomCreation(ticketResponse.ticket_id,
                    onRoomcreated));
            }
            else
            {
                Debug.LogError(request.downloadHandler.text);
            }
        }
    }

    public class TicketRequestBody
    {
        public string package_name;
        public string user_id;
        public string user_name;
        public string wallet_address;
        public string match_environment;
        public int number_of_users;
        public float price;
        public string price_currency;
        public string sku;
        public int match_max_duration;
    }

    public class TicketResponse
    {
        public string ticket_id;
        public string room_id;
    }
}