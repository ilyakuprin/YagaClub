using UnityEngine;

namespace YagaClub
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField, Range(0, 50)] public float HorizontalSpeed { get; private set; }
        [field: SerializeField, Range(0, 50)] public float VerticalSpeed { get; private set; }
    }
}
