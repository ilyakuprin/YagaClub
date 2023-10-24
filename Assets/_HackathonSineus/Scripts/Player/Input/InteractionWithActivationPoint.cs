using System;

namespace YagaClub
{
    public class InteractionWithActivationPoint : IPlayerAction, ISubscribeUnsubscribe
    {
        public event Action<bool> Pressed;

        private readonly PlayerInput _playerInput;
        private ActivityPoint _activityPoint;
        private bool _inTriggerPoint;

        public InteractionWithActivationPoint(PlayerInput playerInput)
            => _playerInput = playerInput;

        public ActivityPoint GetActivityPoint { get => _activityPoint; }

        public void Initialize() => _playerInput.Inputted += Executive;

        public void Executive(InputData inputData)
        {
            if (_inTriggerPoint && inputData.Interaction)
            {
                _activityPoint.FillProgress();
                Pressed?.Invoke(true);
            }
            else
            {
                Pressed?.Invoke(false);
            }
        }

        public void SetActivityPoint(ActivityPoint activityPoint)
        {
            _activityPoint = activityPoint;
            _inTriggerPoint = true;
        }

        public void ForgetActivityPoint()
        {
            if (_inTriggerPoint)
            {
                _activityPoint = null;
                _inTriggerPoint = false;
            }
        }

        public void Dispose() => _playerInput.Inputted -= Executive;
    }
}
