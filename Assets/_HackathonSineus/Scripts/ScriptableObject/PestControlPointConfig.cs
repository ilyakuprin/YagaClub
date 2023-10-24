using UnityEngine;

namespace YagaClub
{
    [CreateAssetMenu(fileName = "PestControlPointConfig", menuName = "Configs/PestControlPointConfig")]
    public class PestControlPointConfig : ScriptableObject
    {
        [field: SerializeField] public CookingObjects CookingObject { get; private set; }
        [field: SerializeField, Range(0, 50)] public float TimeActivation { get; private set; }
        [field: SerializeField, Range(0, 50)] public float TimeOnePercentReductionQuality { get; private set; }
    }
}
