using System;
using Eskills.Service.Repository;

namespace Eskills.Service
{
    public class CreateTicketUseCase
    {
        private readonly TicketRepository _repository;
        private readonly ICoroutineExecutor _executor;

        public CreateTicketUseCase(TicketRepository repository, ICoroutineExecutor executor)
        {
            _repository = repository;
            _executor = executor;
        }

        public void Execute(string ewt, Action<TicketData> success,
            Action<EskillsError> error)
        {
            _executor.StartCoroutine(_repository.CreateTicket(ewt, success, error));
        }
    }
}