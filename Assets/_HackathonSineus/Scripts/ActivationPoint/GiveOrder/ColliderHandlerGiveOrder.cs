using System;
using UnityEngine;
using Zenject;

namespace YagaClub
{
    public class ColliderHandlerGiveOrder : IInitializable, IDisposable
    {
        public event Action<bool> ColliderActivated;

        private readonly GiveOrderPoint _giveOrderPoint;
        private readonly ActivateCollider _activDeactivTriggers;
        private Collider2D _collider;

        public ColliderHandlerGiveOrder(GiveOrderPoint giveOrderPoint,
                                        ActivateCollider activDeactivTriggers)
        {
            _giveOrderPoint = giveOrderPoint;
            _activDeactivTriggers = activDeactivTriggers;
        }

        public void Initialize()
        {
            _collider = _giveOrderPoint.GetComponent<Collider2D>();
            _collider.enabled = false;
            _activDeactivTriggers.ListOver += OnEnableCollider;
            _giveOrderPoint.GetGiveOrderTimer.TimerIsOver += OnDisableTrigger;
        }

        private void OnEnableCollider()
            => SetEnabledCollider(true);

        private void OnDisableTrigger()
            => SetEnabledCollider(false);

        private void SetEnabledCollider(bool value)
        {
            _collider.enabled = value;
            ColliderActivated?.Invoke(value);
        }

        public void Dispose()
            => _activDeactivTriggers.ListOver -= OnEnableCollider;
    }
}
