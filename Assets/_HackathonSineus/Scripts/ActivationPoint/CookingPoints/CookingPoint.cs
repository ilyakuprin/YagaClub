using System;
using UnityEngine;
using Zenject;

namespace YagaClub
{
    [RequireComponent(typeof(Collider2D))]
    public class CookingPoint : ActivityPoint
    {
        public event Action<bool> ColliderActivated;

        [SerializeField] private CookingObjects _cookingObject;
        [SerializeField] private Collider2D _collider;
        private UpdateTimerCooking _timer;
        private RemovingFromList _removingFromList;

        public UpdateTimerCooking GetCookingTimer { get => _timer; }
        public int GetIntCookingObj { get => (int) _cookingObject; }
        public bool IsColliderActive { get => _collider.enabled; }

        [Inject]
        private void Constructor(CookingsObjectConfig config,
                                 RemovingFromList removingFromList)
        {
            ObjectForCoockingConfig currentConf = config.GetObject(_cookingObject);

            _timer = new UpdateTimerCooking(this);
            _timer.Set(currentConf.TimeActivation, currentConf.Cooldown);
            SetTimer(_timer);

            _removingFromList = removingFromList;
        }

        private void Awake() => EnableCollider(false);

        public void EnableCollider(bool value)
        {
            _collider.enabled = value;
            ColliderActivated?.Invoke(value);
        }

        private void DeactivateCollier()
        {
            EnableCollider(false);
            _removingFromList.Remove(GetIntCookingObj);
        }

        private void OnEnable()
            => _timer.TimerIsOver += DeactivateCollier;

        private void OnDisable()
            => _timer.TimerIsOver -= DeactivateCollier;

        private void OnValidate()
            => _collider ??= GetComponent<Collider2D>();
    }
}
