using System;
using UnityEngine;
using Zenject;

namespace YagaClub
{
    public class FuelHandler : MonoBehaviour
    {
        public event Action GameOver;

        private float _totalFuelConsumptionTime;
        private CoroutineTimer _timer;

        public CoroutineTimer GetTimer { get => _timer; }
        public float GetTotalValue { get => _totalFuelConsumptionTime; }

        private readonly float _minValue = 1;

        [Inject]
        private void Construct(FuelConfig fuelConfig)
        {
            _timer = new CoroutineTimer(this);

            _totalFuelConsumptionTime = fuelConfig.TotalFuelConsumptionTime;
        }

        private void Awake()
        {
            SetTime(_totalFuelConsumptionTime);
        }

        public void AddTotalFuel(float value)
        {
            if (value > 0)
                _totalFuelConsumptionTime += value;
        }

        private void SetTime(float value)
        {
            if (value < _minValue)
                value = _minValue;

            _timer.Set(value);
            _timer.StartCountingTime();
        }

        public void AddTime(float time)
        {
            float sumTime = _timer.GetRemainigTime + time;

            if (sumTime > _totalFuelConsumptionTime)
                sumTime = _totalFuelConsumptionTime;

            SetTime(sumTime);
        }

        private void OnGameOver()
            => GameOver?.Invoke();

        private void OnEnable()
            => _timer.TimerIsOver += OnGameOver;

        private void OnDisable()
            => _timer.TimerIsOver -= OnGameOver;
    }
}
