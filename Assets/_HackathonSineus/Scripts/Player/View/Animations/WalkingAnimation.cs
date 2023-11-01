using System;
using UnityEngine;
using Zenject;

namespace YagaClub
{
    public class WalkingAnimation : IInitializable, IDisposable
    {
        private readonly HorizontalMovement _horizontalMovement;
        private readonly Animator _animator;
        private readonly SpriteRenderer _spriteRenderer;
        private readonly int _walk;

        public WalkingAnimation(HorizontalMovement horizontalMovement,
                                Animator animator,
                                SpriteRenderer spriteRenderer,
                                HashingAnimation hashingAnimation)
        {
            _horizontalMovement = horizontalMovement;
            _animator = animator;
            _spriteRenderer = spriteRenderer;
            _walk = hashingAnimation.Walk;
        }

        public void Initialize()
            => _horizontalMovement.Moved += OnMove;

        private void OnMove(float value)
        {
            if (value > 0)
            {
                _spriteRenderer.flipX = false;
                ActivateAnim(true);
                
            }
            else if (value < 0)
            {
                _spriteRenderer.flipX = true;
                ActivateAnim(true);
            }
            else
            {
                ActivateAnim(false);
            }
        }

        private void ActivateAnim(bool value)
            => _animator.SetBool(_walk, value);

        public void Dispose()
            => _horizontalMovement.Moved -= OnMove;
    }
}
