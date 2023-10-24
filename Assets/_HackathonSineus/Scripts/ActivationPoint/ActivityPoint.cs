using UnityEngine;

namespace YagaClub
{
    public abstract class ActivityPoint : MonoBehaviour
    {
        private UpdateTimer _timer;

        public UpdateTimer GetTimer { get => _timer; }

        protected void SetTimer(UpdateTimer timer) => _timer = timer;

        private void Awake()
        {
            Collider2D collider = GetComponent<Collider2D>();
            collider.isTrigger = true;
            collider.enabled = false;
        }

        public void FillProgress() => _timer.UpdateTime();
    }
}
