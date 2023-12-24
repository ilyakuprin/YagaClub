using UnityEngine;
using Zenject;

namespace YagaClub
{
    public class DescentFromPlatform : MonoBehaviour, IPlayerAction
    {
        private PlayerInput _playerInput;
        private readonly float _surfaceAcrDown = 0;
        private readonly float _surfaceAcrUp = 180;
        private PlatformEffector2D _platform = null;

        [Inject]
        private void Construct(PlayerInput playerInput)
        {
            _playerInput = playerInput;
        }

        public void Executive(InputData inputData)
        {
            float inputVerticalDirection = inputData.VerticalDirection;

            if (_platform != null)
            {
                if (inputVerticalDirection < 0)
                    SetArc(true);
                else
                    SetArc(false);
            }
        }

        private void SetArc(bool isGoDown)
        {
            float acr;

            if (isGoDown)
                acr = _surfaceAcrDown;
            else
                acr = _surfaceAcrUp;

            if (_platform.surfaceArc != acr)
                _platform.surfaceArc = acr;
        }

        private void Remember(Transform gameObj)
        {
            if (gameObj.TryGetComponent(out PlatformEffector2D platform))
            {
                _platform = platform;
            }
        }

        private void Forget(Transform gameObj)
        {
            if (_platform != null)
                if (gameObj == _platform.transform)
                {
                    SetArc(false);
                    _platform = null;
                }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Remember(collision.transform);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            Forget(collision.transform);
        }

        private void OnEnable()
        {
            _playerInput.Inputted += Executive;
        }

        private void OnDisable()
        {
            _playerInput.Inputted -= Executive;
        }
    }
}
