using System;
using Zenject;

namespace YagaClub
{
    public class ActivDeactivTriggers : IInitializable, IDisposable
    {
        public event Action TriggerDeactivated;

        private readonly CreatingOrder _creatingOrder;
        private readonly CookingPoint[] _cookingPoints;
        private readonly int _countPoint;

        public ActivDeactivTriggers(CreatingOrder creatingOrder,
                                    CookingPoint[] cookingPoints)
        {
            _creatingOrder = creatingOrder;
            _cookingPoints = cookingPoints;
            _countPoint = cookingPoints.Length;
        }

        public void Initialize()
        {
            for (int i = 0; i < _countPoint; i++)
                _cookingPoints[i].GetCookingTimer.TimerIsOver += OnDeactiveTrigger;

            _creatingOrder.OrderCreated += OnCheckAndActivate;
        }

        private void OnCheckAndActivate()
        {
            for (int i = 0; i < _creatingOrder.GetSizeList; i++)
            {
                CookingPoint currentPoint =
                    _creatingOrder.GetActivityPoint(_creatingOrder.GetDishName(i));

                for (int k = 0; k < _countPoint; k++)
                {
                    if (_cookingPoints[k] == currentPoint)
                    {
                        if (!_cookingPoints[k].IsColliderActive)
                            _cookingPoints[k].ActivateCollider();
                    }
                }
            }
        }

        private void OnDeactiveTrigger(ActivityPoint activityPoint)
        {
            for (int i = 0; i < _countPoint; i++)
            {
                if (_cookingPoints[i] == activityPoint)
                {
                    bool dishOnList = false;

                    for (int k = 0; k < _creatingOrder.GetSizeList; k++)
                    {
                        CookingPoint currentPoint =
                            _creatingOrder.GetActivityPoint(_creatingOrder.GetDishName(k));

                        if (_cookingPoints[i] == currentPoint)
                        {
                            dishOnList = true;
                            break;
                        }
                    }

                    if (!dishOnList)
                    {
                        _cookingPoints[i].DeactivateCollider();
                        TriggerDeactivated?.Invoke();
                    }

                    break;
                }
            }
        }

        public void Dispose()
        {
            _creatingOrder.OrderCreated -= OnCheckAndActivate;

            foreach(var i in _cookingPoints)
                i.GetCookingTimer.TimerIsOver -= OnDeactiveTrigger;
        }
    }
}
