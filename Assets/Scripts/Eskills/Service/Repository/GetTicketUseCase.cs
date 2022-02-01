using System;
using Eskills.Service.Repository;

namespace Eskills.Service
{
    public class GetTicketUseCase
    {
        private readonly TicketRepository _repository;
        private readonly ICoroutineExecutor _executor;

        public GetTicketUseCase(TicketRepository repository, ICoroutineExecutor executor)
        {
            _repository = repository;
            _executor = executor;
        }

        public void Execute(string ewt, string ticketId, Action<TicketData> success,
            Action<EskillsError> error)
        {
            _executor.StartCoroutine(_repository.GETTicketData(ewt, ticketId,success, error));
        }
    }
}