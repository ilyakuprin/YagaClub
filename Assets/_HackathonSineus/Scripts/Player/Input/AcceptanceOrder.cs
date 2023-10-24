using System;
using Zenject;

namespace YagaClub
{
    public class AcceptanceOrder : IPlayerAction, IDisposable, IInitializable
    {
        private readonly PlayerInput _playerInput;
        private readonly OpeningPhone _openingPhone;
        private readonly CreatingOrder _creatingOrder;

        public AcceptanceOrder(PlayerInput playerInput,
                               OpeningPhone openingPhone,
                               CreatingOrder creatingOrder)
        {
            _playerInput = playerInput;
            _openingPhone = openingPhone;
            _creatingOrder = creatingOrder;
        }

        public void Initialize() => _playerInput.Inputted += Executive;

        public void Executive(InputData inputData)
        {
            if (inputData.PressButton && _openingPhone.GetPhoneOpen)
            {
                _creatingOrder.OnCreatOrder();
            }
        }

        public void Dispose() => _playerInput.Inputted -= Executive;
    }
}
