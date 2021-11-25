using System;
using Eskills.Scripts.Service.Repository;

namespace Eskills.Scripts.Service
{
    public class GetRoomInfoUseCase
    {
        private readonly RoomRepository _repository;
        private readonly ICoroutineExecutor _executor;

        public GetRoomInfoUseCase(RoomRepository repository, ICoroutineExecutor executor)
        {
            _repository = repository;
            _executor = executor;
        }

        public void Execute(string session, Action<RoomData> success,
            Action<EskillsError> error)
        {
            _executor.StartCoroutine(_repository.GETRoomData(session, success, error));
        }
    }
}