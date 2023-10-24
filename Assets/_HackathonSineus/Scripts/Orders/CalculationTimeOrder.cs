using System;
using UnityEngine;
using Zenject;

namespace YagaClub
{
    public class CalculationTimeOrder : IInitializable, IDisposable
    {
        public event Action TimeCalculated;

        private readonly ObjectForCoockingConfig[] _orderConfigs;
        private readonly CookingPoint[] _cookingPoints;
        private readonly CreatingOrder _creatingOrder;
        private float _startingAmountTime = 0;

        public CalculationTimeOrder(CookingsObjectConfig cookingsObject,
                                    CookingPoint[] cookingPoints,
                                    CreatingOrder creatingOrder)
        {
            _orderConfigs = cookingsObject.GetObjects();
            _cookingPoints = cookingPoints;
            _creatingOrder = creatingOrder;
        }

        public float GetTime { get => _startingAmountTime; }

        public void Initialize() => _creatingOrder.OrderCreated += OnCalculateTime;

        private void OnCalculateTime()
        {
            _startingAmountTime = 0;

            for (int i = 0; i < _creatingOrder.GetSizeList; i++)
            {
                CookingPoint item =
                    _creatingOrder.GetActivityPoint(_creatingOrder.GetDishName(i));

                for (int k = 0; k < _cookingPoints.Length; k++)
                {
                    if (item == _cookingPoints[k])
                    {
                        _startingAmountTime += _cookingPoints[k].GetCookingTimer.GetTimeActivations *
                                               _orderConfigs[k].ExtraTimeMultiplier;

                        break;
                    }
                }
            }

            _startingAmountTime = Mathf.Round(_startingAmountTime);

            TimeCalculated?.Invoke();
        }

        public void Dispose() => _creatingOrder.OrderCreated -= OnCalculateTime;
    }
}
