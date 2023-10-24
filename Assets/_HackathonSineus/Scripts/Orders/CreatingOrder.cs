using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace YagaClub
{
    public class CreatingOrder : IInitializable, IDisposable
    {
        public event Action OrderCreated;
        public event Action<string> DishRemoved;

        private readonly Random _random = new Random();
        private readonly string[][] _dishes;
        private readonly int _countPoints;
        private readonly CookingPoint[] _cookingPoints;
        private readonly Dictionary<string, CookingPoint> _keyValuePairs =
            new Dictionary<string, CookingPoint>();
        private readonly ObjectForCoockingConfig[] _orderConfigs;
        private readonly AcceptanceOrder _acceptanceOrder;

        private List<string> _listDishNames;

        public CreatingOrder(AcceptanceOrder acceptanceOrder,
                             CookingsObjectConfig cookingsObject,
                             CookingPoint[] cookingPoints)
        {
            _countPoints = cookingsObject.GetSize;
            _dishes = new string[_countPoints][];
            _orderConfigs = cookingsObject.GetObjects();
            _cookingPoints = cookingPoints;
            _acceptanceOrder = acceptanceOrder;
        }

        public int GetSizeList { get => _listDishNames.Count; }

        public void Initialize()
        {
            _acceptanceOrder.OrderAccepted += OnCreatOrder;

            for (int i = 0; i < _countPoints; i++)
                _dishes[i] = _orderConfigs[i].GetCopyDishes();

            FillDictionary();
        }

        public string GetDishName(int index)
        {
            if (index >= 0 && index < _listDishNames.Count)
                return _listDishNames[index];
            else
                return null;
        }

        public void RemoveDishFromList(string value)
        {
            _listDishNames.Remove(value);
            DishRemoved?.Invoke(value);
        }

        public CookingPoint GetActivityPoint(string value) => _keyValuePairs[value];

        public void OnCreatOrder()
        {
            _listDishNames = new List<string>();

            int[] countAllDishes = new int[_countPoints];
            for (int i = 0; i < _countPoints; i++)
                countAllDishes[i] = _random.Next(_orderConfigs[i].MaxNumberItemsPerOrder + 1);

            int max = countAllDishes.Max();

            if (max > 0)
            {
                for (int i = 0; i < max; i++)
                {
                    for (int j = 0; j < _dishes.Length; j++)
                    {
                        if (countAllDishes[j] > 0)
                        {
                            FillOutList(j);
                            countAllDishes[j] -= 1;
                        }
                    }
                }
            }
            else
            {
                FillOutList(_dishes.Length - 1);
            }

            OrderCreated?.Invoke();
        }

        private void FillOutList(int value)
        {
            string randomDish = _dishes[value][_random.Next(_dishes[value].Length)];

            _listDishNames.Add(randomDish);
        }

        private void FillDictionary()
        {
            for (int i = 0; i < _cookingPoints.Length; i++)
                for (int k = 0; k < _dishes[i].Length; k++)
                    _keyValuePairs.Add(_dishes[i][k], _cookingPoints[i]);
        }

        public void Dispose() => _acceptanceOrder.OrderAccepted -= OnCreatOrder;
    }
}
