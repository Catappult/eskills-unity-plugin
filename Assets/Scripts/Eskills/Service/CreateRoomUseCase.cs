using System;
using Eskills.Service.Repository;

namespace Eskills.Service
{
    public class CreateRoomUseCase
    {
        private readonly ICoroutineExecutor _executor;
        private readonly TicketRepository _ticketRepository;
        private readonly string _privateKey;
        private readonly RoomRepository _roomRepository;


        public CreateRoomUseCase(ICoroutineExecutor executor, TicketRepository ticketRepository, string privateKey,
            RoomRepository roomRepository
        )
        {
            _executor = executor;
            _ticketRepository = ticketRepository;
            _privateKey = privateKey;
            _roomRepository = roomRepository;
        }

        public void Execute(string userName, float value, string currency, string product, int timeout,
            MatchEnvironment matchEnvironment, int numberOfPlayers, Action<string> onRoomcreated)
        {
            _executor.StartCoroutine(_ticketRepository.CreateTicket(userName, value, currency, product, timeout,
                matchEnvironment, numberOfPlayers,
                (ticketId, roomId) =>
                {
                    _executor.StartCoroutine(_roomRepository.login(_privateKey, ticketId, roomId, onRoomcreated));
                }));
        }
    }
}