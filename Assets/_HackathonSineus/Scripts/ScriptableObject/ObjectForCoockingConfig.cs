using UnityEngine;

namespace YagaClub
{
    [CreateAssetMenu(fileName = "ObjectForCoocking", menuName = "Configs/ObjectForCoocking")]
    public class ObjectForCoockingConfig : ScriptableObject
    {
        [field: SerializeField] public CookingObjects CookingObject { get; private set; }
        [Space]
        [SerializeField] private string[] _dishes;        
        [field: SerializeField, Range(0, 50)] public float TimeActivation { get; private set; }
        [field: SerializeField, Range(0, 50)] public float Cooldown { get; private set; }
        [field: SerializeField, Range(0, 50)] public int MaxNumberItemsPerOrder { get; private set; }
        [field: SerializeField, Range(0, 500)] public int Cost { get; private set; }
        [field: SerializeField, Range(0, 1)] public float CostReductionMultiplier { get; private set; }
        [field: SerializeField, Range(1, 20)] public float ExtraTimeMultiplier { get; private set; }

        public string[] GetCopyDishes()
        {
            string[] dishes = new string[_dishes.Length];
            _dishes.CopyTo(dishes, 0);

            return dishes;
        }
    }
}
