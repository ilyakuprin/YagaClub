using System;
using UnityEngine;
using Zenject;

namespace YagaClub
{
    [RequireComponent(typeof(Collider2D))]
    public class PestControlPoint : ActivityPoint
    {
        public event Action<bool> ColliderActivated;

        [SerializeField] private CookingObjects _cookingObject;
        [SerializeField] private Collider2D _collider;
        private readonly UpdateTimerNoCooldown _timer = new UpdateTimerNoCooldown();

        public int GetIntCookingObj { get => (int)_cookingObject; }
        public CookingObjects GetCookingObj { get => _cookingObject; }
        public UpdateTimerNoCooldown GetNoCooldpwnTimer { get => _timer; }
        public bool GetEnabledCollider { get => _collider.enabled; }

        [Inject]
        private void Constructor(PestsControlPointsConfig pestsControlPointsConfig)
        {
            PestControlPointConfig config = pestsControlPointsConfig.GetObject(_cookingObject);

            SetTimer(_timer);
            //_timer.Set(config.TimeActivation);
        }

        private void Awake() => EnabledCollider(false);

        public void ActivateCollider(bool value)
        {
            EnabledCollider(value);
            ColliderActivated?.Invoke(value);
        }

        private void DeactivateCollider() => ActivateCollider(false);

        private void EnabledCollider(bool value) => _collider.enabled = value;

        private void OnEnable() => _timer.TimerIsOver += DeactivateCollider;

        private void OnDisable() => _timer.TimerIsOver -= DeactivateCollider;

        private void OnValidate() => _collider ??= GetComponent<Collider2D>();
    }
}
