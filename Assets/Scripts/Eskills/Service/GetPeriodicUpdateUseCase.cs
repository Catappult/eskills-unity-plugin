using System;
using Eskills.Service.Repository;
using System.Collections;
using UnityEngine;
namespace Eskills.Service
{
    public class GetPeriodicUpdateUseCase
    {
        private readonly RoomRepository _repository;
        private readonly ICoroutineExecutor _executor;
        private bool periodicUpdate;
        public GetPeriodicUpdateUseCase(RoomRepository repository, ICoroutineExecutor executor)
        {
            _repository = repository;
            _executor = executor;
            periodicUpdate = false;
        }

        public void Execute(string session, Action<RoomData> success,
            Action<EskillsError> error)
        {
            var coroutine = GetPeriodicUpdate(session,success,error);
            _executor.StartCoroutine(coroutine);
        }

        private IEnumerator GetPeriodicUpdate(string session, Action<RoomData> success, Action<EskillsError> error)
        {   
            this.periodicUpdate = true;
            while (periodicUpdate)
            {
                _executor.StartCoroutine(_repository.GETRoomData(session, success, error));
                yield return new WaitForSeconds(5.0f);
            }
        }

        public void Stop()
        {
            this.periodicUpdate = false;
        }
    }
}