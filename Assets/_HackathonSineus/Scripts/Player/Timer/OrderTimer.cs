using UnityEngine;
using Zenject;

namespace YagaClub
{
    public class OrderTimer : MonoBehaviour
    {
        private CalculationTimeOrder _calculationTime;
        private RewardCompletingOrder _rewardCompletingOrder;
        private GiveOrderPoint _giveOrderPoint;
        private CoroutineTimer _timer;

        public CoroutineTimer GetTimer { get => _timer; }

        [Inject]
        private void Constructor(CalculationTimeOrder calculationTime,
                                 RewardCompletingOrder rewardCompletingOrder,
                                 GiveOrderPoint giveOrderPoint)
        {
            _calculationTime = calculationTime;
            _rewardCompletingOrder = rewardCompletingOrder;
            _giveOrderPoint = giveOrderPoint;

            _timer = new CoroutineTimer(this);
        }

        private void StartTimer()
        {
            _timer.Set(_calculationTime.GetTime);
            _timer.StartCountingTime();
        }

        private void StopTimer() => _timer.StopCountingTime();

        private void OnEnable()
        {
            _calculationTime.TimeCalculated += StartTimer;
            _timer.TimerIsOver += _rewardCompletingOrder.On—ut—ost;
            _giveOrderPoint.GetGiveOrderTimer.TimerIsOver += StopTimer;
        }

        private void OnDisable()
        {
            _calculationTime.TimeCalculated -= StartTimer;
            _giveOrderPoint.GetGiveOrderTimer.TimerIsOver -= StopTimer;
        }
    }
}
