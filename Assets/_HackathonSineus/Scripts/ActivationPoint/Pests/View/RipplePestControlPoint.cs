using UnityEngine;

namespace YagaClub
{
    public class RipplePestControlPoint : RipplePoint
    {
        [SerializeField] private PestControlPoint _pestControlPoint;

        private void OnEnable()
            => _pestControlPoint.ColliderActivated += CallTrigger;

        private void OnDisable()
            => _pestControlPoint.ColliderActivated -= CallTrigger;
    }
}
