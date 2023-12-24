using UnityEngine;
using Zenject;

namespace YagaClub
{
    public class PestsHandler : MonoBehaviour
    {
        private PestsControlPointsConfig _pests;
        private CoroutineTimer _timer;
        private CreatingOrder _creatingOrder;
        private PestControlPoint[] _pestPoints;
        private CookingPoint[] _cookingPoints;
        private int _indexCorrespondingCooking;
        private bool _lockTimerStarted;

        private readonly System.Random _random = new System.Random();

        [Inject]
        private void Constructor(CreatingOrder creatingOrder,
                                 PestControlPoint[] pestPoints,
                                 CookingPoint[] cookingPoints,
                                 PestsControlPointsConfig pestsControlPointsConfig)
        {
            _timer = new CoroutineTimer(this);
            _creatingOrder = creatingOrder;
            _pestPoints = pestPoints;
            _cookingPoints = cookingPoints;
            _pests = pestsControlPointsConfig;
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
                float time = Random.Range(_pests.MinTimeSpawn, _pests.MaxTimeSpawn);
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
                int cookingObject = _creatingOrder.GetCookingObject(dish);

                int countPoints = _pestPoints.Length;

                for (int i = 0; i < countPoints; i++)
                {
                    if (_pestPoints[i].GetIntCookingObj == cookingObject)
                    {
                        _pestPoints[i].ActivateCollider(true);
                        _cookingPoints[i].EnableCollider(false);
                        _indexCorrespondingCooking = i;

                        break;
                    }
                }
            }
            else
                UnlockTimerStart();
        }

        private void PestDeactivation()
        {
            _cookingPoints[_indexCorrespondingCooking].EnableCollider(true);
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
