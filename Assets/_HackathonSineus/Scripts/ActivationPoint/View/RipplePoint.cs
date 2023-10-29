using UnityEngine;

namespace YagaClub
{
    [RequireComponent(typeof(Animator))]
    public abstract class RipplePoint : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private int _cashParameter;

        private void Awake()
            => _cashParameter = new StringAnimationParameters().Change;

        protected void CallTrigger(bool value)
            => _animator.SetBool(_cashParameter, value);

        private void OnValidate()
            => _animator ??= GetComponent<Animator>();
    }
}
