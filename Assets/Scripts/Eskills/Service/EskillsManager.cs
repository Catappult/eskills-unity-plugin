using System;
using Eskills.Scripts;
using Eskills.Service.Repository;
using JetBrains.Annotations;

namespace Eskills.Service
{
    public class EskillsManager
    {
        private readonly PurchaseActivity _purchaseActivity;
        private readonly EndgameActivity _endgameActivity;
        private readonly GetRoomInfoUseCase _getRoomInfoUseCase;
        private readonly SetScoreUseCase _setScoreUseCase;
        private readonly GetPeriodicUpdateUseCase _getPeriodicUpdateUseCase;

        public EskillsManager(PurchaseActivity purchaseActivity, EndgameActivity endgameActivity,
         GetRoomInfoUseCase getRoomInfoUseCase,SetScoreUseCase setScoreUseCase,
          GetPeriodicUpdateUseCase getPeriodicUpdateUseCase)
        {
            _purchaseActivity = purchaseActivity;
            _endgameActivity = endgameActivity;
            _getRoomInfoUseCase = getRoomInfoUseCase;
            _setScoreUseCase = setScoreUseCase;
            _getPeriodicUpdateUseCase = getPeriodicUpdateUseCase;
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
            if(status == SetScoreBody.Status.COMPLETED) {
                _endgameActivity.start(session);
            }
        }

        public void GetPeriodicUpdate(string session, Action<RoomData> success, Action<EskillsError> error)
        {
             _getPeriodicUpdateUseCase.Execute(session, success, error);
        }

        public void StopPeriodicUpdate()
        {
            _getPeriodicUpdateUseCase.Stop();
        }
    }
}