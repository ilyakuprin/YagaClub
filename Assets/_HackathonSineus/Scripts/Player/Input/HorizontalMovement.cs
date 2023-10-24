using UnityEngine;
using Zenject;

namespace YagaClub
{
    public class HorizontalMovement : IPlayerAction, ISubscribeUnsubscribe, IFixedTickable
    {
        public delegate void ToMove(float direction);
        public event ToMove Moved;
        
        private readonly PlayerInput _playerInput;
        private readonly Rigidbody2D _rigidbody;
        private readonly float _speed;

        private float _horizontalForce;

        public HorizontalMovement(PlayerInput playerInput, Rigidbody2D rigidbody, PlayerConfig playerConfig)
        {
            _playerInput = playerInput;
            _rigidbody = rigidbody;
            _speed = playerConfig.HorizontalSpeed;
        }

        public void Initialize() => _playerInput.Inputted += Executive;

        public void Executive(InputData inputData)
        {
            float inputHorizontalDirection = inputData.HorizontalDirection;

            if (!StaticFormulas.IsValueZero(inputHorizontalDirection))
            {
                _horizontalForce = inputHorizontalDirection * _speed;
            }
            else if (_horizontalForce != 0)
            {
                _horizontalForce = 0;
            }

            Moved?.Invoke(inputHorizontalDirection);
        }

        public void FixedTick() => _rigidbody.velocity = new Vector2(_horizontalForce, _rigidbody.velocity.y);

        public void Dispose()
        {
            _playerInput.Inputted -= Executive;
            _horizontalForce = 0;
        }
    }
}
