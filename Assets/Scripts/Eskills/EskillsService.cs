using System;
using Eskills.Scripts;
using Eskills.Service;
using Eskills.Service.Repository;
using Eskills.Ui;
using UnityEngine;

namespace Eskills
{
    public class EskillsService : MonoBehaviour, ICoroutineExecutor
    {
        private EskillsManager _eskillsManager;
        [SerializeField] public UserNameProvider _userNameProvider;


        void Awake()
        {
            var roomRepository = new RoomRepository(new RoomResponseMapper());
            var TicketRepository = new TicketRepository(new TicketResponseMapper());
            var getRoomInfoUseCase = new GetRoomInfoUseCase(roomRepository, this);
            var setScoreUseCase = new SetScoreUseCase(roomRepository, this);
            var getTicketUseCase = new GetTicketUseCase(TicketRepository, this);
            var createTicketUseCase = new CreateTicketUseCase(TicketRepository, this);
            _eskillsManager = new EskillsManager(new PurchaseActivity(), getRoomInfoUseCase, setScoreUseCase, getTicketUseCase, createTicketUseCase);
        }


        public void StartPurchase(MatchParameters matchParameters)
        {
            if (Application.platform != RuntimePlatform.Android) return;
            string userName;
            if (_userNameProvider == null)
            {
                userName = "";
                Debug.LogWarning("You should provide an implementation of " + nameof(UserNameProvider) +
                                 " before calling StartPurchase method. An empty user name was used");
            }
            else
            {
                userName = _userNameProvider.GetUserName();
            }

            _eskillsManager.StartPurchase(userName, matchParameters.value, matchParameters.currency,
                matchParameters.product, matchParameters.timeout, matchParameters.matchEnvironment,
                matchParameters.numberOfPlayers);
        }


        public void GetRoomInfo(string session, Action<RoomData> success, Action<EskillsError> error)
        {
            _eskillsManager.GetRoomInfo(session, success, error);
        }

        public void SetScore(string session, SetScoreBody.Status status, int score, Action<RoomData> success = null,
            Action<EskillsError> error = null)
        {
            _eskillsManager.SetScore(session, status, score, success, error);
        }

        public void GetTicket(string ewt, string ticket_id, Action<TicketData> success, Action<EskillsError> error)
        {
            _eskillsManager.GetTicket(ewt, ticket_id, success, error);
        }

        public void CreateTicket(string ewt, Action<TicketData> success, Action<EskillsError> error)
        {
            _eskillsManager.CreateTicket(ewt, success, error);
        }
    }
}