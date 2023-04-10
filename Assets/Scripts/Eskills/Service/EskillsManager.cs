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
        private readonly CreateRoomUseCase _createRoomUseCase;
        private readonly GetPeriodicUpdateUseCase _getPeriodicUpdateUseCase;

        public EskillsManager(PurchaseActivity purchaseActivity, GetRoomInfoUseCase getRoomInfoUseCase,
            SetScoreUseCase setScoreUseCase, GetPeriodicUpdateUseCase getPeriodicUpdateUseCase, CreateRoomUseCase createRoomUseCase)
        {
            _purchaseActivity = purchaseActivity;
            _getRoomInfoUseCase = getRoomInfoUseCase;
            _setScoreUseCase = setScoreUseCase;
            _createRoomUseCase = createRoomUseCase;
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
        }

        public void GetPeriodicUpdate(string session, Action<RoomData> success, Action<EskillsError> error)
        {
             _getPeriodicUpdateUseCase.Execute(session, success, error);
        }

        public void StopPeriodicUpdate()
        {
            _getPeriodicUpdateUseCase.Stop();
        }

        public void CreateRoom(string userName, float value, string currency,
            string product, int timeout, MatchEnvironment matchEnvironment,
            int numberOfPlayers, Action<string> onRoomcreated)
        {
            _createRoomUseCase.Execute(userName, value, currency, product, timeout, matchEnvironment, numberOfPlayers,
                onRoomcreated);
        }
    }
}