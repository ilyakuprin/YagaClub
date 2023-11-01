using System;
using UnityEngine;
using Zenject;

namespace YagaClub
{
    public class MovementOnStairAnimation : IInitializable, IDisposable
    {
        private readonly VerticalMovement _verticalMovement;
        private readonly Animator _animator;
        private readonly int _onLadder;

        public MovementOnStairAnimation(VerticalMovement verticalMovement,
                                Animator animator,
                                HashingAnimation hashingAnimation)
        {
            _verticalMovement = verticalMovement;
            _animator = animator;
            _onLadder = hashingAnimation.OnLadder;
        }

        public void Initialize()
            => _verticalMovement.Moved += OnMove;

        private void OnMove()
        {
            if (_verticalMovement.IsLadder)
                ActivateAnim(true);
            else
                ActivateAnim(false);
        }

        private void ActivateAnim(bool value)
            => _animator.SetBool(_onLadder, value);

        public void Dispose()
            => _verticalMovement.Moved -= OnMove;
    }
}
