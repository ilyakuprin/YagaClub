using UnityEngine;
using Zenject;

namespace YagaClub
{
    public class PestsHandler : MonoBehaviour
    {
        [SerializeField] private float _minTime;
        [SerializeField] private float _maxTime;
        private CoroutineTimer _timer;
        private CreatingOrder _creatingOrder;
        private PestControlPoint[] _pestPoints;
        private CookingPoint[] _cookingPoints;
        private int _indexÑorrespondingCooking;
        private bool _lockTimerStarted;

        private readonly System.Random _random = new System.Random();

        [Inject]
        private void Constructor(CreatingOrder creatingOrder,
                                 PestControlPoint[] pestPoints,
                                 CookingPoint[] cookingPoints)
        {
            _timer = new CoroutineTimer(this);
            _creatingOrder = creatingOrder;
            _pestPoints = pestPoints;
            _cookingPoints = cookingPoints;
        }

        private void Awake() => ArrayMappingCookingObj();

        private void ArrayMappingCookingObj()
        {
            int countPoints = _pestPoints.Length;

            CookingPoint[] sortCookingPoints = new CookingPoint[countPoints];

            for (int i = 0; i < countPoints; i++)
                for (int k = 0; k < countPoints; k++)
                    if (_pestPoints[i].GetIntCookingObj == _cookingPoints[k].GetIntCookingObj)
                    {
                        sortCookingPoints[i] = _cookingPoints[k];
                        break;
                    }

            _cookingPoints = sortCookingPoints;
        }

        private void OnStartPestSpawnTimer()
        {
            if (!_lockTimerStarted)
            {
                _lockTimerStarted = true;
                float time = Random.Range(_minTime, _maxTime);
                _timer.Set(time);
                _timer.StartCountingTime();
            }
        }

        private void OnPestActivation()
        {
            if (_creatingOrder.GetSizeList > 0)
            {
                int index = _random.Next(_creatingOrder.GetSizeList);
                string dish = _creatingOrder.GetDishName(index);
                CookingPoint cookingPoint = _creatingOrder.GetActivityPoint(dish);
                int cookingObject = cookingPoint.GetIntCookingObj;

                for (int i = 0; i < _pestPoints.Length; i++)
                {
                    if (_pestPoints[i].GetIntCookingObj == cookingObject)
                    {
                        _pestPoints[i].ActivateCollider();
                        _cookingPoints[i].DeactivateCollider();
                        _indexÑorrespondingCooking = i;
                        break;
                    }
                }
            }
        }

        private void PestDeactivation()
        {
            _cookingPoints[_indexÑorrespondingCooking].ActivateCollider();
            UnlockTimerStart();
            OnStartPestSpawnTimer();
        }

        private void UnlockTimerStart() => _lockTimerStarted = false;

        private void OnEnable()
        {
            _creatingOrder.OrderCreated += OnStartPestSpawnTimer;
            _timer.TimerIsOver += OnPestActivation;

            foreach (PestControlPoint point in _pestPoints)
                point.GetNoCooldpwnTimer.TimerIsOver += PestDeactivation;
        }

        private void OnDisable()
        {
            _creatingOrder.OrderCreated -= OnStartPestSpawnTimer;
            _timer.TimerIsOver -= OnPestActivation;

            foreach (PestControlPoint point in _pestPoints)
                point.GetNoCooldpwnTimer.TimerIsOver -= PestDeactivation;
        }
    }
}
