using UnityEngine;

namespace YagaClub
{
    public class DescentFromStairs : MonoBehaviour
    {
        [SerializeField] private PlatformEffector2D _platform;
        private readonly float _surfaceAcrDown = 0;
        private readonly float _surfaceAcrUp = 180;

        private void GoDown()
        {
            _platform.surfaceArc = _surfaceAcrDown;
        }

        private void GoUp()
        {
            _platform.surfaceArc = _surfaceAcrUp;
        }
    }
}
