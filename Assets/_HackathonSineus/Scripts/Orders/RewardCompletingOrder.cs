using System;
using UnityEngine;
using Zenject;

namespace YagaClub
{
    public class RewardCompletingOrder : IInitializable, IDisposable
    {
        public event Action RewardCalculated;
        public event Action RewardReduced;

        private readonly CreatingOrder _creatingOrder;
        private readonly CookingPoint[] _cookingPoints;
        private readonly ObjectForCoockingConfig[] _orderConfigs;
        private int _finalReward;
        private int _reduceReward;
        private bool _isTimeOver;
        private readonly int _oneHundred = 100;

        public RewardCompletingOrder(CreatingOrder creatingOrder,
                                     CookingPoint[] cookingPoints,
                                     CookingsObjectConfig cookingsObject)
        {
            _creatingOrder = creatingOrder;
            _cookingPoints = cookingPoints;
            _orderConfigs = cookingsObject.GetObjects();
        }

        public int GetReward
        {
            get
            {
                if (_isTimeOver)
                    return _reduceReward;
                else
                    return _finalReward;
            }
        }

        public void Initialize() => _creatingOrder.OrderCreated += OnCalculateStartReward;

        public void OnReduceReward(int cookingObj, float countPercent)
        {
            for (int i = 0; i < _cookingPoints.Length; i++)
            {
                if (_cookingPoints[i].GetIntCookingObj == cookingObj)
                {
                    float value = _orderConfigs[i].Cost * (countPercent / _oneHundred);

                    if (!_isTimeOver)
                        _finalReward = Mathf.RoundToInt(_finalReward - value);

                    _reduceReward = Mathf.RoundToInt(_reduceReward - 
                        value * _orderConfigs[i].CostReductionMultiplier);
                }
            }

            RewardCalculated?.Invoke();
        }

        private void OnCalculateStartReward()
        {
            ResetValue();

            for (int i = 0; i < _creatingOrder.GetSizeList; i++)
            {
                CookingPoint item =
                    _creatingOrder.GetActivityPoint(_creatingOrder.GetDishName(i));

                for (int k = 0; k < _cookingPoints.Length; k ++)
                {
                    if (item == _cookingPoints[k])
                    {
                        _finalReward += _orderConfigs[k].Cost;
                        _reduceReward += Mathf.RoundToInt(_orderConfigs[k].Cost *
                                        _orderConfigs[k].CostReductionMultiplier);
                        break;
                    }
                }
            }

            RewardCalculated?.Invoke();
        }

        public void OnÑutÑost()
        {
            _isTimeOver = true;
            RewardReduced?.Invoke();
        }

        private void ResetValue()
        {
            _finalReward = 0;
            _reduceReward = 0;
            _isTimeOver = false;
        }

        public void Dispose() => _creatingOrder.OrderCreated -= OnCalculateStartReward;
    }
}
