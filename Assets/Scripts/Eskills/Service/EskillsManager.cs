using System;
using Eskills.Scripts;
using Eskills.Service.Repository;
using JetBrains.Annotations;
using Eskills.Ui;

namespace Eskills.Service
{
    public class EskillsManager
    {
        private readonly PurchaseActivity _purchaseActivity;
        private readonly GetRoomInfoUseCase _getRoomInfoUseCase;
        private readonly SetScoreUseCase _setScoreUseCase;
        private readonly GetTicketUseCase _getTicketUseCase;
        private readonly JoinQueueUseCase _joinQueueUseCase;
        private readonly CreateTicketUseCase _createTicketUseCase;
        private readonly LoginUseCase _loginUseCase;

        public EskillsManager(PurchaseActivity purchaseActivity, GetRoomInfoUseCase getRoomInfoUseCase,
            SetScoreUseCase setScoreUseCase, GetTicketUseCase getTicketUseCase, JoinQueueUseCase joinQueueUseCase, CreateTicketUseCase createTicketUseCase,LoginUseCase loginUseCase)
        {
            _purchaseActivity = purchaseActivity;
            _getRoomInfoUseCase = getRoomInfoUseCase;
            _setScoreUseCase = setScoreUseCase;
            _getTicketUseCase = getTicketUseCase;
            _joinQueueUseCase = joinQueueUseCase;
            _createTicketUseCase = createTicketUseCase;
            _loginUseCase = loginUseCase;
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

        public void JoinQueue(string ewt, MatchParameters matchParameters, Action<string> success, Action<EskillsError> error)
        {
            _joinQueueUseCase.Execute(ewt, matchParameters,success, error);
        }

        public void GetTicket(string ewt, string ticketId, Action<TicketData> success, Action<EskillsError> error)
        {
            _getTicketUseCase.Execute(ewt, ticketId, success, error);
        }
    }
}