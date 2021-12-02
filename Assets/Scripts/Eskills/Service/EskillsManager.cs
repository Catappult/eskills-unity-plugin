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

        public EskillsManager(PurchaseActivity purchaseActivity, GetRoomInfoUseCase getRoomInfoUseCase,
            SetScoreUseCase setScoreUseCase)
        {
            _purchaseActivity = purchaseActivity;
            _getRoomInfoUseCase = getRoomInfoUseCase;
            _setScoreUseCase = setScoreUseCase;
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
    }
}