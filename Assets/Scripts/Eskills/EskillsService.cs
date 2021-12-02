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


        void Start()
        {
            var roomRepository = new RoomRepository(new RoomResponseMapper());
            var getRoomInfoUseCase = new GetRoomInfoUseCase(roomRepository, this);
            var setScoreUseCase = new SetScoreUseCase(roomRepository, this);
            _eskillsManager = new EskillsManager(new PurchaseActivity(), getRoomInfoUseCase, setScoreUseCase);
        }


        public void StartPurchase(MatchParameters matchParameters)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                _eskillsManager.StartPurchase(matchParameters.userName, matchParameters.value, matchParameters.currency,
                    matchParameters.product, matchParameters.timeout, matchParameters.matchEnvironment,
                    matchParameters.numberOfPlayers);
            }
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
    }
}