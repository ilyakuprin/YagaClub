using UnityEngine;

namespace YagaClub
{
    public abstract class ActivityPoint : MonoBehaviour
    {
        private UpdateTimer _timer;

        public UpdateTimer GetTimer { get => _timer; }

        protected void SetTimer(UpdateTimer timer) => _timer = timer;

        public void FillProgress() => _timer.UpdateTime();
    }
}
