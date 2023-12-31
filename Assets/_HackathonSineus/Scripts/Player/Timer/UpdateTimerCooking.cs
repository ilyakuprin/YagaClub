using System;
using System.Collections;
using UnityEngine;

namespace YagaClub
{
    public class UpdateTimerCooking : UpdateTimer
    {
        public event Action TimerIsOver;
        public event Action<int> CooldownOver;

        private readonly CookingPoint _cookingPoint;
        private float _cooldown;

        public UpdateTimerCooking(CookingPoint cookingPoint)
            => _cookingPoint = cookingPoint;

        public void SetCooldown(float cooldown)
        {
            if (cooldown > 0)
            {
                _cooldown = cooldown;
            }
            else
            {
                _cooldown = 0;
            }
        }

        protected override void TimerExpired()
        {
            _cookingPoint.StartCoroutine(WaitCooldown());
            TimerIsOver?.Invoke();
        }

        private IEnumerator WaitCooldown()
        {
            yield return new WaitForSeconds(_cooldown);
            CooldownOver?.Invoke(_cookingPoint.GetIntCookingObj);
            ResetCounter();
        }
    }
}
