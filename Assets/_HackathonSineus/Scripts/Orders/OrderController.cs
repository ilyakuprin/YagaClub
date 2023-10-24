using System;
using Zenject;

namespace YagaClub
{
    public class OrderController : IInitializable, IDisposable
    {
        private readonly CreatingOrder _creatingOrder;
        private readonly AcceptanceOrder _acceptanceOrder;
        private readonly GiveOrderPoint _giveOrderPoint;

        public OrderController(CreatingOrder creatingOrder,
                               AcceptanceOrder acceptanceOrder,
                               GiveOrderPoint giveOrderPoint)
        {
            _creatingOrder = creatingOrder;
            _acceptanceOrder = acceptanceOrder;
            _giveOrderPoint = giveOrderPoint;
        }
        public void Initialize()
        {
            _acceptanceOrder.OrderAccepted += OnAcceptOrder;
            _giveOrderPoint.GetGiveOrderTimer.TimerIsOver += OnGiveOrder;
        }

        private void OnAcceptOrder()
            => _acceptanceOrder.Dispose();

        private void OnGiveOrder()
            => _acceptanceOrder.Initialize();

        public void Dispose()
        {
            _acceptanceOrder.OrderAccepted -= OnAcceptOrder;
            _giveOrderPoint.GetGiveOrderTimer.TimerIsOver -= OnGiveOrder;
        }
    }
}
