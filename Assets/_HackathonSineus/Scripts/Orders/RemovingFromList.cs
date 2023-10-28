using System;

namespace YagaClub
{
    public class RemovingFromList
    {
        public event Action RowDeleted;

        private readonly CreatingOrder _creatingOrder;

        public RemovingFromList(CreatingOrder creatingOrder)
            => _creatingOrder = creatingOrder;

        public void Remove(int cookingObject)
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
    }
}
