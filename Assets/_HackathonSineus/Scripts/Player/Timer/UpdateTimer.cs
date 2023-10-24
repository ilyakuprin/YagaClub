using System;
using UnityEngine;

namespace YagaClub
{
    public abstract class UpdateTimer
    {
        public event Action<float> HasBeenUpdated;

        private float _time;
        private float _remainigTime;

        public float GetTimeActivations { get => _time; }
        public float GetRemainigTime { get => _remainigTime; }

        protected void SetTime(float time = 1) => _time = time;

        protected void ResetCounter() => _remainigTime = 0;

        public void UpdateTime()
        {
            if (_remainigTime < _time)
            {
                _remainigTime += Time.deltaTime;
                HasBeenUpdated?.Invoke(_remainigTime);

                if (_remainigTime >= _time)
                    TimerExpired();
            }
        }

        protected abstract void TimerExpired();
    }
}
