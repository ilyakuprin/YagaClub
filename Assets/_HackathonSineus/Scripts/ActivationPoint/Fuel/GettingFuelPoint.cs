using System;
using UnityEngine;
using Zenject;

namespace YagaClub
{
    [RequireComponent(typeof(Collider2D))]
    public class GettingFuelPoint : ActivityPoint
    {
        public event Action<bool> TookFuel;

        [SerializeField] private Collider2D _collider;
        private FuelHandler _fuelHandler;
        private float _extraFuelTime;
        private readonly UpdateTimerNoCooldown _noCooldownTimer = new UpdateTimerNoCooldown();

        public UpdateTimerNoCooldown GetNoCooldownTimer { get => _noCooldownTimer; }

        [Inject]
        private void Construct(FuelHandler fuelHandler, FuelConfig fuelConfig)
        {
            _noCooldownTimer.Set(fuelConfig.TimeActivation);
            SetTimer(_noCooldownTimer);

            _fuelHandler = fuelHandler;
            _extraFuelTime = fuelConfig.ExtraFuelTime;
        }

        private void Awake() => _collider.enabled = false;

        private void EnabledCollider(bool value)
        {
            _collider.enabled = value;
            TookFuel?.Invoke(value);
        }

        private void OnAddFuel() => _fuelHandler.AddTime(_extraFuelTime);

        private void OnDisableCollider() => EnabledCollider(false);

        public void OnEnableCollider() => EnabledCollider(true);

        private void OnEnable()
        {
            _noCooldownTimer.TimerIsOver += OnAddFuel;
            _noCooldownTimer.TimerIsOver += OnDisableCollider;
        }

        private void OnDisable()
        {
            _noCooldownTimer.TimerIsOver -= OnAddFuel;
            _noCooldownTimer.TimerIsOver += OnDisableCollider;
        }

        private void OnValidate() => _collider ??= GetComponent<Collider2D>();
    }
}
