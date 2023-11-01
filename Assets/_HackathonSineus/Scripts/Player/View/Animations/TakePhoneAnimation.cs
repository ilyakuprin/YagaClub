using System;
using UnityEngine;
using Zenject;

namespace YagaClub
{
    public class TakePhoneAnimation : IInitializable, IDisposable
    {
        private readonly OpeningPhone _openingPhone;
        private readonly Animator _animator;
        private readonly int _phoneOpen;

        public TakePhoneAnimation(OpeningPhone openingPhone,
                                  Animator animator,
                                  HashingAnimation hashingAnimation)
        {
            _openingPhone = openingPhone;
            _animator = animator;
            _phoneOpen = hashingAnimation.PhoneOpen;
        }

        public void Initialize()
            => _openingPhone.PressedPhone += OnMove;

        private void OnMove(bool value)
        {
            if (value)
                ActivateAnim(true);
            else
                ActivateAnim(false);
        }

        private void ActivateAnim(bool value)
            => _animator.SetBool(_phoneOpen, value);

        public void Dispose()
            => _openingPhone.PressedPhone -= OnMove;
    }
}
