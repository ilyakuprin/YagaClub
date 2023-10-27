using UnityEngine;
using Zenject;

namespace YagaClub
{
    public class FuelHandler : MonoBehaviour
    {
        private float _totalFuelConsumptionTime;
        private CoroutineTimer _timer;

        [Inject]
        private void Construct(FuelConfig fuelConfig)
        {
            _timer = new CoroutineTimer(this);

            _totalFuelConsumptionTime = fuelConfig.TotalFuelConsumptionTime;
            SetTimer(_totalFuelConsumptionTime);
        }

        private void Start() => StartTimer();

        private void SetTimer(float value) => _timer.Set(value);

        private void StartTimer() => _timer.StartCountingTime();

        public void AddTime(float time)
        {
            float sumTime = _timer.GetRemainigTime + time;

            if (sumTime > _totalFuelConsumptionTime)
                sumTime = _totalFuelConsumptionTime;

            SetTimer(sumTime);
            StartTimer();
        }

        private void Update()
        {
            Debug.Log("Текущее топливо: " + Mathf.RoundToInt(_timer.GetRemainigTime));
        }

        private void OnGameOver() => Debug.Log("Топливо закончилось");

        private void OnEnable() => _timer.TimerIsOver += OnGameOver;

        private void OnDisable() => _timer.TimerIsOver -= OnGameOver;
    }
}
