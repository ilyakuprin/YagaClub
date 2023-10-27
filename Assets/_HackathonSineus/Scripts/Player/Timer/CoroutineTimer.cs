using System;
using System.Collections;
using UnityEngine;

namespace YagaClub
{
    public class CoroutineTimer
    {
        public event Action<float> HasBeenUpdated;
        public event Action TimerIsOver;

        private float _time;
        private float _remainigTime;
        private Coroutine _countdown;

        private readonly MonoBehaviour _monoBeh;

        public float GetRemainigTime { get => _remainigTime; }

        public CoroutineTimer(MonoBehaviour monoBeh)
            => _monoBeh = monoBeh;

        public void Set(float time)
            => _time = time;

        public void StartCountingTime()
        {
            StopCountingTime();
            ResetCounter();
            _countdown = _monoBeh.StartCoroutine(CountUp());
        }

        public void StopCountingTime()
        {
            if (_countdown != null)
                _monoBeh.StopCoroutine(_countdown);
        }

        private IEnumerator CountUp()
        {
            while (_remainigTime > 0)
            {
                _remainigTime -= Time.deltaTime;
                HasBeenUpdated?.Invoke(_remainigTime);
                yield return null;
            }

            ResetCounter();
            TimerIsOver?.Invoke();
        }

        private void ResetCounter() => _remainigTime = _time;
    }
}
