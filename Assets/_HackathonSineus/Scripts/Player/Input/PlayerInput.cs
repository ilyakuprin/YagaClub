using System;
using UnityEngine;
using Zenject;

namespace YagaClub
{
    public class PlayerInput : ITickable
    {
        public event Action<InputData> Inputted;

        private InputData _inputData = new InputData();

        public void Tick()
        {
            float horizontal = Input.GetAxisRaw(ConstantsInput.HORIZONTAL);
            float vertical = Input.GetAxisRaw(ConstantsInput.VERTICAL);
            bool interaction = Input.GetButton(ConstantsInput.INTERACTION);
            bool openPhone = Input.GetButtonDown(ConstantsInput.OPENPHONE);
            bool cancel = Input.GetButtonDown(ConstantsInput.CANCEL);
            bool pressbutton = Input.GetButtonDown(ConstantsInput.PRESSBUTTON);

            _inputData = new InputData
            {
                HorizontalDirection = horizontal,
                VerticalDirection = vertical,
                Interaction = interaction,
                OpenPhone = openPhone,
                Cancel = cancel,
                PressButton = pressbutton
            };

            Inputted?.Invoke(_inputData);
        }
    }
}
