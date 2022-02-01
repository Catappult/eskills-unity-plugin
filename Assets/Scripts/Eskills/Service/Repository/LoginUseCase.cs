using System;
using Eskills.Service.Repository;

namespace Eskills.Service
{
    public class LoginUseCase
    {
        private readonly RoomRepository _repository;
        private readonly ICoroutineExecutor _executor;


        public LoginUseCase(RoomRepository repository, ICoroutineExecutor executor)
        {
            _repository = repository;
            _executor = executor;
        }

        public void Execute(string ewt, string ticketId, string roomId, Action<string> success, Action<EskillsError> error)
        {
            _executor.StartCoroutine(_repository.Login(ewt, ticketId, roomId,success, error));    
        }
    }
}