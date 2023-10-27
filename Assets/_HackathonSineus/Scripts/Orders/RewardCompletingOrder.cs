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
        private float _finalReward;
        private float _reduceReward;
        private bool _isTimeOver;
        private readonly float _oneProcent = 0.01f;

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
                    return Mathf.RoundToInt(_reduceReward);
                else
                    return Mathf.RoundToInt(_finalReward);
            }
        }

        public void Initialize() => _creatingOrder.OrderCreated += OnCalculateStartReward;

        public void OnReduceReward(int cookingObj)
        {
            for (int i = 0; i < _cookingPoints.Length; i++)
            {
                if (_cookingPoints[i].GetIntCookingObj == cookingObj)
                {
                    float value = _orderConfigs[i].Cost * _oneProcent;

                    if (!_isTimeOver)
                        _finalReward = _finalReward - value;

                    _reduceReward = _reduceReward - 
                        value * _orderConfigs[i].CostReductionMultiplier;

                    break;
                }
            }

            RewardCalculated?.Invoke();
        }

        private void OnCalculateStartReward()
        {
            ResetValue();

            for (int i = 0; i < _creatingOrder.GetSizeList; i++)
            {
                int cookingObject =
                    _creatingOrder.GetCookingObject(_creatingOrder.GetDishName(i));

                for (int k = 0; k < _cookingPoints.Length; k ++)
                {
                    if (cookingObject == _cookingPoints[k].GetIntCookingObj)
                    {
                        _finalReward += _orderConfigs[k].Cost;
                        _reduceReward += _orderConfigs[k].Cost *
                                        _orderConfigs[k].CostReductionMultiplier;
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
