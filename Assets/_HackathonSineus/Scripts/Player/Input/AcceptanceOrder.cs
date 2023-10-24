using System;
using Zenject;

namespace YagaClub
{
    public class AcceptanceOrder : IPlayerAction, IDisposable, IInitializable
    {
        public event Action OrderAccepted;

        private readonly PlayerInput _playerInput;
        private readonly OpeningPhone _openingPhone;

        public AcceptanceOrder(PlayerInput playerInput,
                               OpeningPhone openingPhone)
        {
            _playerInput = playerInput;
            _openingPhone = openingPhone;
        }

        public void Initialize() => _playerInput.Inputted += Executive;

        public void Executive(InputData inputData)
        {
            if (inputData.PressButton && _openingPhone.GetPhoneOpen)
            {
                InvokeOrderAccepted();
            }
        }

        public void InvokeOrderAccepted() => OrderAccepted?.Invoke();

        public void Dispose() => _playerInput.Inputted -= Executive;
    }
}
