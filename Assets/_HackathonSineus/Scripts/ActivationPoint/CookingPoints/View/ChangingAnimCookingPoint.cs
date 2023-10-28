using UnityEngine;

namespace YagaClub
{
    [RequireComponent(typeof(Animator), typeof(CookingPoint))]
    public class ChangingAnimCookingPoint : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private CookingPoint _cookingPoint;
        private int _cashParameter;

        private void Awake()
            => _cashParameter = new StringAnimationParameters().Change;

        private void CallTrigger(bool value)
            => _animator.SetBool(_cashParameter, value);

        private void OnEnable()
            => _cookingPoint.ColliderActivated += CallTrigger;

        private void OnDisable()
            => _cookingPoint.ColliderActivated -= CallTrigger;

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
            _cookingPoint ??= GetComponent<CookingPoint>();
        }
    }
}
