using System;
using Eskills.Service.Repository;
using System.Collections;
using UnityEngine;
using Eskills.Ui;

namespace Eskills.Service
{
    public class JoinQueueUseCase
    {
        private readonly ICoroutineExecutor _executor;
        private readonly CreateTicketUseCase _createTicketUseCase;
        private readonly LoginUseCase _loginUseCase;
        private readonly GetTicketUseCase _getTicketUseCase;
        private readonly ParameterMapper _parameterMapper;
        private bool periodicUpdate;

        public JoinQueueUseCase(ICoroutineExecutor executor, CreateTicketUseCase createTicketUseCase ,LoginUseCase loginUseCase, GetTicketUseCase getTicketUseCase)
        {
            _executor = executor;
            _createTicketUseCase = createTicketUseCase;
            _loginUseCase = loginUseCase;
            _getTicketUseCase = getTicketUseCase;
            periodicUpdate = false;
            _parameterMapper = new ParameterMapper();
        }

        public void Execute(string ewt, MatchParameters matchParameters,Action<string> success,
            Action<EskillsError> error)
        {   
            TicketParameters ticketParameters = _parameterMapper.Map(matchParameters);
            _createTicketUseCase.Execute(ewt,ticketParameters, ticket => {
                var coroutine = GetTicketUpdate(ewt, ticket.ticketId,
                ticket => {
                    Debug.Log("Updating ticket status");
                    if(ticket.ticketStatus == TicketStatus.COMPLETED){
                        Stop();
                        _loginUseCase.Execute(ewt,ticket.ticketId,ticket.roomId,success,error);
                    }
                },
                error
                );
                _executor.StartCoroutine(coroutine);
            }, error);    
        }

         private IEnumerator GetTicketUpdate(string ewt,string ticketId, Action<TicketData> success, Action<EskillsError> error)
        {
            periodicUpdate = true;
            while (periodicUpdate)
            {
                _getTicketUseCase.Execute(ewt,ticketId,success,error);
                yield return new WaitForSeconds(5.0f);
            }
        }

        public void Stop()
        {
            periodicUpdate = false;
        }
    }
}