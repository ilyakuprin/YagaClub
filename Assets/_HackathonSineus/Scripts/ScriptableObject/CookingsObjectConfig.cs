using System;
using System.Linq;
using UnityEngine;

namespace YagaClub
{
    [CreateAssetMenu(fileName = "CookingsObjectConfig", menuName = "Configs/CookingsObjectConfig")]
    public class CookingsObjectConfig : ScriptableObject
    {
        [SerializeField] private ObjectForCoockingConfig[] _objectsForCoocking;

        public int GetSize { get => _objectsForCoocking.Length; }

        public ObjectForCoockingConfig GetObject(CookingObjects cookingObjects)
        {
            foreach (var i in _objectsForCoocking)
                if (i.CookingObject == cookingObjects)
                    return i;

            return null;
        }

        public ObjectForCoockingConfig[] GetObjects()
        {
            ObjectForCoockingConfig[] objects = new ObjectForCoockingConfig[GetSize];
            _objectsForCoocking.CopyTo(objects, 0);

            return objects;
        }

        private void OnValidate()
        {
            int enumCount = Enum.GetNames(typeof(CookingObjects)).Length;

            if (_objectsForCoocking.Length != enumCount)
                _objectsForCoocking = new ObjectForCoockingConfig[enumCount];

            ObjectForCoockingConfig[] duplicates = _objectsForCoocking.GroupBy(coockObj => coockObj)
                .Where(group => group.Count() > 1)
                .Select(group => group.Key)
                .ToArray();

            foreach (var item in duplicates)
                Debug.LogError(item.ToString());
        }
    }
}
