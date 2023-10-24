using System;
using Zenject;

namespace YagaClub
{
    public class OpeningPhone : IPlayerAction, IDisposable, IInitializable
    {
        public delegate void PressPhone(bool isOpen);
        public event PressPhone PressedPhone;

        private readonly PlayerInput _playerInput;
        private readonly ISubscribeUnsubscribe[] _switchableComponents;

        private bool _phoneOpen = false;

        public OpeningPhone(PlayerInput playerInput, ISubscribeUnsubscribe[] switchableComponents)
        {
            _playerInput = playerInput;
            _switchableComponents = switchableComponents;
        }

        public bool GetPhoneOpen { get => _phoneOpen; }

        public void Initialize() => _playerInput.Inputted += Executive;

        public void Executive(InputData inputData)
        {
            if (inputData.OpenPhone)
            {
                if (!_phoneOpen)
                {
                    DisableComponents();
                    PressedPhone?.Invoke(true);
                }
                else
                {
                    EnableComponents();
                    PressedPhone?.Invoke(false);
                }
            }
        }

        private void DisableComponents()
        {
            foreach (var i in _switchableComponents)
                i.Dispose();

            _phoneOpen = true;
        }

        private void EnableComponents()
        {
            foreach (var i in _switchableComponents)
                i.Initialize();

            Dispose();
            Initialize();

            _phoneOpen = false;
        }

        public void Dispose() => _playerInput.Inputted -= Executive;
    }
}
