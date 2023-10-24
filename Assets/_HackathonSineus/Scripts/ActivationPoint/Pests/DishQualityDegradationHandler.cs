using UnityEngine;
using Zenject;

namespace YagaClub
{
    [RequireComponent(typeof(PestControlPoint))]
    public class DishQualityDegradationHandler : MonoBehaviour
    {
        [SerializeField] private PestControlPoint _pestControlPoint;
        private CoroutineTimer _timer;
        private int _counter = 0;
        private readonly int _maxCount = 100;

        private RewardCompletingOrder _rewardCompletingOrder;

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
        }

        private void StartTimer() => _timer.StartCountingTime();

        private void StopTimer() => _timer.StopCountingTime();

        private void AddCounter()
        {
            _counter++;
            Debug.Log(_counter);                                                               // нужен ResetCounter где-то
            _rewardCompletingOrder.OnReduceReward(_pestControlPoint.GetIntCookingObj,
                                                  _counter);

            if (_counter < _maxCount)
                StartTimer();
        }

        private void ResetCounter() => _counter = 0;                                            // нужен ResetCounter где-то

        private void OnEnable()
        {
            _pestControlPoint.ColliderActivated += StartTimer;
            _pestControlPoint.ColliderDeactivated += StopTimer;
            _timer.TimerIsOver += AddCounter;
        }

        private void OnDisable()
        {
            _pestControlPoint.ColliderActivated -= StartTimer;
            _pestControlPoint.ColliderDeactivated -= StopTimer;
            _timer.TimerIsOver -= AddCounter;
        }

        private void OnValidate()
            => _pestControlPoint ??= GetComponent<PestControlPoint>();
    }
}
