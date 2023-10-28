using System;
using Zenject;

namespace YagaClub
{
    public class ActivDeactivTriggers : IInitializable, IDisposable
    {
        public event Action ListOver;

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
            {
                _cookingPoints[i].GetCookingTimer.TimerIsOver += OnTryActivateGiveOrder;
                _cookingPoints[i].GetCookingTimer.CooldownOver += OnTryActivateTrigger;
            }

            _creatingOrder.OrderCreated += OnCheckAndActivate;
        }

        private void OnCheckAndActivate()
        {
            for (int i = 0; i < _creatingOrder.GetSizeList; i++)
            {
                int cookingObj = _creatingOrder.GetCookingObject(_creatingOrder.GetDishName(i));

                for (int k = 0; k < _countPoint; k++)
                {
                    if (_cookingPoints[k].GetIntCookingObj == cookingObj)
                    {
                        if (!_cookingPoints[k].IsColliderActive)
                            _cookingPoints[k].EnableCollider(true);
                    }
                }
            }
        }

        private void OnTryActivateTrigger(int cookingObject)
        {
            for (int i = 0; i < _countPoint; i++)
            {
                if (_cookingPoints[i].GetIntCookingObj == cookingObject)
                {
                    for (int k = 0; k < _creatingOrder.GetSizeList; k++)
                    {
                        int cookingObjInOrder =
                            _creatingOrder.GetCookingObject(_creatingOrder.GetDishName(k));

                        if (_cookingPoints[i].GetIntCookingObj == cookingObjInOrder)
                        {
                            _cookingPoints[i].EnableCollider(true);
                            break;
                        }
                    }

                    break;
                }
            }
        }

        private void OnTryActivateGiveOrder()
        {
            if (_creatingOrder.GetSizeList == 0)
                ListOver?.Invoke();
        }

        public void Dispose()
        {
            _creatingOrder.OrderCreated -= OnCheckAndActivate;

            foreach (var i in _cookingPoints)
            {
                i.GetCookingTimer.TimerIsOver -= OnTryActivateGiveOrder;
                i.GetCookingTimer.CooldownOver -= OnTryActivateTrigger;
            }
        }
    }
}
