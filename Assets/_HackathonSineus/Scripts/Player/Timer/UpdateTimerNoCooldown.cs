using System;

namespace YagaClub
{
    public class UpdateTimerNoCooldown : UpdateTimer
    {
        public event Action TimerIsOver;

        public void Set(float time)
        {
            if (time > 0)
                SetTime(time);
            else
                SetTime();
        }

        protected override void TimerExpired()
        {
            TimerIsOver?.Invoke();
            ResetCounter();
        }
    }
}
