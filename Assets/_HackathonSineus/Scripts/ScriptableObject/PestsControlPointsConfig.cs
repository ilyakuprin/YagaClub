using System;
using System.Linq;
using UnityEngine;

namespace YagaClub
{
    [CreateAssetMenu(fileName = "PestsControlPointsConfig", menuName = "Configs/PestsControlPointsConfig")]
    public class PestsControlPointsConfig : ScriptableObject
    {
        [SerializeField] private PestControlPointConfig[] _pestControlPointConfig;

        public int GetSize { get => _pestControlPointConfig.Length; }

        public PestControlPointConfig GetObject(CookingObjects cookingObjects)
        {
            foreach (var i in _pestControlPointConfig)
                if (i.CookingObject == cookingObjects)
                    return i;

            return null;
        }

        public PestControlPointConfig[] GetObjects()
        {
            PestControlPointConfig[] objects = new PestControlPointConfig[GetSize];
            _pestControlPointConfig.CopyTo(objects, 0);

            return objects;
        }

        private void OnValidate()
        {
            int enumCount = Enum.GetNames(typeof(CookingObjects)).Length;

            if (_pestControlPointConfig.Length != enumCount)
                _pestControlPointConfig = new PestControlPointConfig[enumCount];

            PestControlPointConfig[] duplicates = _pestControlPointConfig.GroupBy(coockObj => coockObj)
                .Where(group => group.Count() > 1)
                .Select(group => group.Key)
                .ToArray();

            foreach (var item in duplicates)
                Debug.LogError(item.ToString());
        }
    }
}
