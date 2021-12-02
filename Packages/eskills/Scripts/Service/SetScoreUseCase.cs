using System;
using Eskills.Scripts.Service.Repository;
using JetBrains.Annotations;

namespace Eskills.Scripts.Service
{
    public class SetScoreUseCase
    {
        private readonly RoomRepository _roomRepository;
        private readonly ICoroutineExecutor _executor;

        public SetScoreUseCase(RoomRepository roomRepository, ICoroutineExecutor executor)
        {
            _roomRepository = roomRepository;
            _executor = executor;
        }

        public void Execute(string session, SetScoreBody.Status status, int score,
            [CanBeNull] Action<RoomData> success, [CanBeNull] Action<EskillsError> error)
        {
            _executor.StartCoroutine(_roomRepository.SetScore(session, status, score, success, error));
        }
    }
}