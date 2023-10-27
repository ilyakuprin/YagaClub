using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace YagaClub
{
    public class VisualizationQualityColumns : MonoBehaviour
    {
        [SerializeField] private CookingObjects _cookingObjects;
        [SerializeField] private Image _column;
        private DishQualityDegradationHandler _dishQuality;
        private CreatingOrder _creatingOrder;
        private List<int> _qualityDish;
        private int _indexDish;
        private int _intCookingObject;
        private readonly int _oneHundred = 100;

        [Inject]
        private void Construct(DishQualityDegradationHandler[] dishsQuality,
                               CreatingOrder creatingOrder)
        {
            _intCookingObject = (int)_cookingObjects;

            foreach (var dishQuality in dishsQuality)
                if (_intCookingObject == dishQuality.GetPestControlPoint.GetIntCookingObj)
                {
                    _dishQuality = dishQuality;
                    break;
                }

            _creatingOrder = creatingOrder;
        }

        private void OnFillList()
        {
            _qualityDish = new List<int>();
            _indexDish = 0;

            for (int i = 0; i < _creatingOrder.GetSizeList; i++)
            {
                string dish = _creatingOrder.GetDishName(i);

                if (_intCookingObject == _creatingOrder.GetCookingObject(dish))
                {
                    _qualityDish.Add(_oneHundred);
                }
            }

            if (_qualityDish.Count > 0)
                FillMaxColumn(1);
        }

        private void FillMaxColumn(float value) => _column.fillAmount = value;

        private void OnChangeValue()
        {
            _qualityDish[_indexDish]--;
            Average();
        }

        private void Average()
        {
            float valueProcent = _qualityDish.Sum() / _qualityDish.Count;
            FillMaxColumn(valueProcent / _oneHundred);
        }

        private void OnEnable()
        {
            _creatingOrder.OrderCreated += OnFillList;
            _dishQuality.CounterAdded += OnChangeValue;
        }

        private void OnDisable()
        {
            _creatingOrder.OrderCreated -= OnFillList;
            _dishQuality.CounterAdded -= OnChangeValue;
        }
    }
}
