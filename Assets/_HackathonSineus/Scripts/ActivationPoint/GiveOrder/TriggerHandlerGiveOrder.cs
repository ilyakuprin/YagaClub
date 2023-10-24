using System;
using UnityEngine;
using Zenject;

namespace YagaClub
{
    public class TriggerHandlerGiveOrder : IInitializable, IDisposable
    {
        private readonly GiveOrderPoint _giveOrderPoint;
        private readonly ActivDeactivTriggers _activDeactivTriggers;
        private readonly CreatingOrder _creatingOrder;
        private Collider2D _collider2D;

        public TriggerHandlerGiveOrder(GiveOrderPoint giveOrderPoint,
                                       ActivDeactivTriggers activDeactivTriggers,
                                       CreatingOrder creatingOrder)
        {
            _giveOrderPoint = giveOrderPoint;
            _activDeactivTriggers = activDeactivTriggers;
            _creatingOrder = creatingOrder;
        }

        public void Initialize()
        {
            _collider2D = _giveOrderPoint.GetComponent<Collider2D>();
            _activDeactivTriggers.TriggerDeactivated += OnTryEnableTrigger;
            _giveOrderPoint.GetGiveOrderTimer.TimerIsOver += OnDisableTrigger;
        }

        private void OnTryEnableTrigger()
        {
            if (_creatingOrder.GetSizeList == 0)
                SetEnabledCollider(true);
        }

        private void OnDisableTrigger()
            => SetEnabledCollider(false);

        private void SetEnabledCollider(bool value)
            => _collider2D.enabled = value;

        public void Dispose()
            => _activDeactivTriggers.TriggerDeactivated -= OnTryEnableTrigger;
    }
}
