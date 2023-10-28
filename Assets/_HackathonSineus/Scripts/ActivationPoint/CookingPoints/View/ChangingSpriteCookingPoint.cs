using UnityEngine;

namespace YagaClub
{
    [RequireComponent(typeof(SpriteRenderer), typeof(CookingPoint))]
    public class ChangingSpriteCookingPoint : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private CookingPoint _cookingPoint;
        [SerializeField] private Sprite _active;
        [SerializeField] private Sprite _inactive;

        private void CallTrigger(bool value)
        {
            if (value)
                _spriteRenderer.sprite = _active;
            else
                _spriteRenderer.sprite = _inactive;
        }

        private void OnEnable()
            => _cookingPoint.ColliderActivated += CallTrigger;

        private void OnDisable()
            => _cookingPoint.ColliderActivated -= CallTrigger;

        private void OnValidate()
        {
            _spriteRenderer ??= GetComponent<SpriteRenderer>();
            _cookingPoint ??= GetComponent<CookingPoint>();
        }
    }
}
