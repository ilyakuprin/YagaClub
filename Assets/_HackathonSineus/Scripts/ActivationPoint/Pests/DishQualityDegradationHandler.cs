using System;
using UnityEngine;
using Zenject;

namespace YagaClub
{
    [RequireComponent(typeof(PestControlPoint))]
    public class DishQualityDegradationHandler : MonoBehaviour
    {
        public event Action CounterAdded;

        [SerializeField] private PestControlPoint _pestControlPoint;
        private CoroutineTimer _timer;
        private int _counter = 0;
        private RewardCompletingOrder _rewardCompletingOrder;
        private CookingPoint _cookingPoint;

        private readonly int _maxCount = 100;

        public PestControlPoint GetPestControlPoint { get => _pestControlPoint; }

        [Inject]
        private void Constructor(PestsControlPointsConfig pestsControlPointsConfig,
                                 RewardCompletingOrder rewardCompletingOrder,
                                 CookingPoint[] cookingPoints)
        {
            PestControlPointConfig config =
                pestsControlPointsConfig.GetObject(_pestControlPoint.GetCookingObj);

            _timer = new CoroutineTimer(this);
            _timer.Set(config.TimeOnePercentReductionQuality);

            _rewardCompletingOrder = rewardCompletingOrder;

            foreach (var i in cookingPoints)
                if (i.GetIntCookingObj == _pestControlPoint.GetIntCookingObj)
                    _cookingPoint = i;
        }

        private void StartTimer() => _timer.StartCountingTime();

        private void StopTimer() => _timer.StopCountingTime();

        private void AddCounter()
        {
            _counter++;

            if (_counter < _maxCount)
            {
                _rewardCompletingOrder.OnReduceReward(_pestControlPoint.GetIntCookingObj);
                CounterAdded?.Invoke();
                StartTimer();
            }
        }

        private void ResetCounter() => _counter = 0;

        private void OnEnable()
        {
            _pestControlPoint.ColliderActivated += StartTimer;
            _pestControlPoint.ColliderDeactivated += StopTimer;
            _timer.TimerIsOver += AddCounter;
            _cookingPoint.GetCookingTimer.TimerIsOver += ResetCounter;
        }

        private void OnDisable()
        {
            _pestControlPoint.ColliderActivated -= StartTimer;
            _pestControlPoint.ColliderDeactivated -= StopTimer;
            _timer.TimerIsOver -= AddCounter;
            _cookingPoint.GetCookingTimer.TimerIsOver -= ResetCounter;
        }

        private void OnValidate()
            => _pestControlPoint ??= GetComponent<PestControlPoint>();  
    }
}
