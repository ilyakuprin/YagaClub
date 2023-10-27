using System;
using System.Collections;
using UnityEngine;

namespace YagaClub
{
    public class UpdateTimerCooking : UpdateTimer
    {
        public event Action<int> TimerIsOver;

        private readonly CookingPoint _cookingPoint;
        private float _cooldown;

        public UpdateTimerCooking(CookingPoint cookingPoint)
            => _cookingPoint = cookingPoint;

        public void Set(float time, float cooldown)
        {
            if (time > 0 && cooldown >= 0)
            {
                SetTime(time);
                _cooldown = cooldown;
            }
            else
            {
                SetTime();
                _cooldown = 0;
            }
        }

        protected override void TimerExpired()
        {
            _cookingPoint.StartCoroutine(WaitCooldown());
            TimerIsOver?.Invoke(_cookingPoint.GetIntCookingObj);
        }

        private IEnumerator WaitCooldown()
        {
            yield return new WaitForSeconds(_cooldown);
            ResetCounter();
        }
    }
}
