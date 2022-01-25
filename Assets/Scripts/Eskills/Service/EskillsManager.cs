using System;
using Eskills.Scripts;
using Eskills.Service.Repository;
using JetBrains.Annotations;

namespace Eskills.Service
{
    public class EskillsManager
    {
        private readonly PurchaseActivity _purchaseActivity;
        private readonly GetRoomInfoUseCase _getRoomInfoUseCase;
        private readonly SetScoreUseCase _setScoreUseCase;
        private readonly GetTicketUseCase _getTicketUseCase;
        private readonly CreateTicketUseCase _createTicketUseCase;
        public EskillsManager(PurchaseActivity purchaseActivity, GetRoomInfoUseCase getRoomInfoUseCase,
            SetScoreUseCase setScoreUseCase, GetTicketUseCase getTicketUseCase, CreateTicketUseCase createTicketUseCase)
        {
            _purchaseActivity = purchaseActivity;
            _getRoomInfoUseCase = getRoomInfoUseCase;
            _setScoreUseCase = setScoreUseCase;
            _getTicketUseCase = getTicketUseCase;
            _createTicketUseCase = createTicketUseCase;
        }


        public void StartPurchase(string userName, float value, string currency, string product, int timeout,
            MatchEnvironment matchEnvironment, int numberOfPlayers)
        {
            _purchaseActivity.start(userName, value, currency, product, timeout, matchEnvironment, numberOfPlayers);
        }

        public void GetRoomInfo(string session, Action<RoomData> success, Action<EskillsError> error)
        {
            _getRoomInfoUseCase.Execute(session, success, error);
        }

        public void SetScore(string session, SetScoreBody.Status status, int score,
            [CanBeNull] Action<RoomData> success, [CanBeNull] Action<EskillsError> error)
        {
            _setScoreUseCase.Execute(session, status, score, success, error);
        }

        public void CreateTicket(string ewt, Action<TicketData> success, Action<EskillsError> error)
        {
            _createTicketUseCase.Execute(ewt, success, error);
        }

        public void GetTicket(string ewt, string ticketId, Action<TicketData> success, Action<EskillsError> error)
        {
            _getTicketUseCase.Execute(ewt, ticketId, success, error);
        }
    }
}