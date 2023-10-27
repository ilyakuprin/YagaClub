using UnityEngine;
using Zenject;

namespace YagaClub
{
    [RequireComponent(typeof(Collider2D))]
    public class CookingPoint : ActivityPoint
    {
        [SerializeField] private CookingObjects _cookingObject;
        [SerializeField] private Collider2D _collider;
        private UpdateTimerCooking _timer;

        public UpdateTimerCooking GetCookingTimer { get => _timer; }

        public int GetIntCookingObj { get => (int) _cookingObject; }

        public bool IsColliderActive { get => _collider.enabled; }

        [Inject]
        private void Constructor(CookingsObjectConfig config)
        {
            ObjectForCoockingConfig currentConf = config.GetObject(_cookingObject);

            _timer = new UpdateTimerCooking(this);
            _timer.Set(currentConf.TimeActivation, currentConf.Cooldown);
            SetTimer(_timer);
        }

        private void Awake() => DeactivateCollider();

        public void ActivateCollider() => _collider.enabled = true;

        public void DeactivateCollider() => _collider.enabled = false;

        private void OnValidate() => _collider ??= GetComponent<Collider2D>();
    }
}
