using UnityEngine;

namespace YagaClub
{
    [CreateAssetMenu(fileName = "ActivationPointConfig", menuName = "Configs/ActivationPointConfig")]
    public class ActivationPointConfig : ScriptableObject
    {
        [field: SerializeField, Range(0, 50)] public float TimeActivation { get; private set; }
    }
}
