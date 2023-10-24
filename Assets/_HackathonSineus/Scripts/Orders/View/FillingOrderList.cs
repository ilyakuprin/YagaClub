using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace YagaClub
{
    public class FillingOrderList : MonoBehaviour
    {
        [SerializeField] private RectTransform _container;
        [SerializeField] private Text _prefabLine;
        private CreatingOrder _creatingOrder;
        private ChangeSizeContainer _changeSize;
        private List<string> dishes;

        [Inject]
        private void Constructor(CreatingOrder creatingOrder)
        {
            _creatingOrder = creatingOrder;
            _changeSize = new ChangeSizeContainer(_container);
        }

        public void OnFillOutContainer()
        {
            dishes = new List<string>();

            for (int i = 0; i < _creatingOrder.GetSizeList; i++)
            {
                Text line = Instantiate(_prefabLine, _container);
                string dishName = _creatingOrder.GetDishName(i);
                line.text = dishName;
                dishes.Add(dishName);
            }

            _changeSize.ChangeSize(_container.childCount);
        }

        private void OnDelDish(string dish)
        {
            for (int i = 0; i < dishes.Count; i++)
            {
                if (dish == dishes[i])
                {
                    dishes.RemoveAt(i);
                    Destroy(_container.GetChild(i).gameObject);
                    _changeSize.ChangeSize(_container.childCount - 1);
                    break;
                }
            }
        }

        private void OnEnable()
        {
            _creatingOrder.OrderCreated += OnFillOutContainer;
            _creatingOrder.DishRemoved += OnDelDish;
        }

        private void OnDisable()
        { 
            _creatingOrder.OrderCreated -= OnFillOutContainer;
            _creatingOrder.DishRemoved -= OnDelDish;
        }
    }
}
