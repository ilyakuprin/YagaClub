using UnityEngine;
using Zenject;

namespace YagaClub
{
    [RequireComponent(typeof(Collider2D))]
    public class GiveOrderPoint : ActivityPoint
    {
        private float _time;
        private readonly UpdateTimerNoCooldown _timer = new UpdateTimerNoCooldown();

        public UpdateTimerNoCooldown GetGiveOrderTimer { get => _timer; }

        [Inject]
        private void Constructor(ActivationPointConfig pointConfig)
        {
            _time = pointConfig.TimeActivation;

            _timer.Set(_time);
            SetTimer(_timer);
        }
    }
}
