using UnityEngine;
using Zenject;

namespace YagaClub
{
    public class TimerActivationGettingFuelPoint : MonoBehaviour
    {
        private CoroutineTimer _timer;
        private GettingFuelPoint _gettingFuelPoint;

        [Inject]
        private void Construct(FuelConfig fuelConfig,
                               GettingFuelPoint gettingFuelPoint)
        {
            _timer = new CoroutineTimer(this);

            float timeInterval = fuelConfig.TimeIntervalReceivingFuel;
            _timer.Set(timeInterval);

            _gettingFuelPoint = gettingFuelPoint;
        }

        private void Start() => StartTimer();

        private void StartTimer() => _timer.StartCountingTime();

        private void Update()
        {
            Debug.Log("Времени до поялвения канситры: " + Mathf.RoundToInt(_timer.GetRemainigTime));
        }

        private void OnEnable()
        {
            _gettingFuelPoint.GetNoCooldownTimer.TimerIsOver += StartTimer;
            _timer.TimerIsOver += _gettingFuelPoint.OnEnableCollider;
        }

        private void OnDisable()
        {
            _gettingFuelPoint.GetNoCooldownTimer.TimerIsOver += StartTimer;
            _timer.TimerIsOver -= _gettingFuelPoint.OnEnableCollider;
        }
    }
}
