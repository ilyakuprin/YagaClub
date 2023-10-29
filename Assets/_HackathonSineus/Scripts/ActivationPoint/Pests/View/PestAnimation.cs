using UnityEngine;

namespace YagaClub
{
    public class PestAnimation : MonoBehaviour
    {
        [SerializeField] private Animator[] _pests;
        [SerializeField] private PestControlPoint _pestControlPoint;
        private int _cashParameter;

        private void Awake()
            => _cashParameter = new StringAnimationParameters().Change;

        private void CallTrigger(bool value)
        {
            foreach (var animator in _pests)
                animator.SetBool(_cashParameter, value);
        }

        private void OnEnable()
            => _pestControlPoint.ColliderActivated += CallTrigger;

        private void OnDisable()
            => _pestControlPoint.ColliderActivated -= CallTrigger;
    }
}
