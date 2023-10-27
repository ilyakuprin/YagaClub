using System;
using Zenject;

namespace YagaClub
{
    public class RemovingFromList : IInitializable, IDisposable
    {
        public event Action RowDeleted;

        private readonly CreatingOrder _creatingOrder;
        private readonly CookingPoint[] _cookingPoint;

        public RemovingFromList(CreatingOrder creatingOrder, CookingPoint[] cookingPoint)
        {
            _creatingOrder = creatingOrder;
            _cookingPoint = cookingPoint;
        }

        public void Initialize()
        {
            foreach (var i in _cookingPoint)
                i.GetCookingTimer.TimerIsOver += Remove;
        }

        private void Remove(int cookingObject)
        {
            for (int i = 0; i < _creatingOrder.GetSizeList; i++)
            {
                string value = _creatingOrder.GetDishName(i);

                if (_creatingOrder.GetCookingObject(value) == cookingObject)
                {
                    _creatingOrder.RemoveDishFromList(value);
                    RowDeleted?.Invoke();
                    break;
                }
            }
        }

        public void Dispose()
        {
            foreach (var i in _cookingPoint)
                i.GetCookingTimer.TimerIsOver += Remove;
        }
    }
}
