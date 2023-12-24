using System;
using UnityEngine;
using Zenject;

namespace YagaClub
{
    public class VerticalMovement : IPlayerAction, ISubscribeUnsubscribe, IFixedTickable
    {
        public event Action Moved;

        private readonly PlayerInput _playerInput;
        private readonly Rigidbody2D _rigidbody;
        private readonly float _speed; 

        private bool _isLadder;
        private float _verticalForce;

        public VerticalMovement(PlayerInput playerInput,
                                Rigidbody2D rigidbody,
                                PlayerConfig playerConfig)
        {
            _playerInput = playerInput;
            _rigidbody = rigidbody;
            _speed = playerConfig.VerticalSpeed;
        }

        public bool IsLadder { get => _isLadder; }

        public void Initialize()
            => _playerInput.Inputted += Executive;

        public void Executive(InputData inputData)
        {
            float inputVerticalDirection = inputData.VerticalDirection;

            if (_isLadder && !StaticFormulas.IsValueZero(inputVerticalDirection) && inputVerticalDirection > 0)
            {
                _verticalForce = inputVerticalDirection * _speed;
            }
            else if (_verticalForce != 0)
            {
                _verticalForce = 0;
            }

            Moved?.Invoke();
        }

        public void FixedTick()
            => _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _verticalForce);

        public void StayOnLadder(bool isLadder)
            => _isLadder = isLadder;

        public void Dispose()
        {
            _playerInput.Inputted -= Executive;
            _verticalForce = 0;
        }
    }
}
