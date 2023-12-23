using System.Collections;
using UnityEngine;
using Zenject;

namespace YagaClub
{
    public class LiftingPhone : MonoBehaviour
    {
        [SerializeField] private RectTransform _phone;
        [SerializeField] private OpeningPhone _openingPhone;
        [SerializeField] private RectTransform _openPhonePosition;
        [SerializeField] private RectTransform _closePhonePosition;
        [SerializeField] private float _timeSec;

        private Coroutine _movePhone;

        [Inject]
        private void Constructor(OpeningPhone openingPhone)
            => _openingPhone = openingPhone;

        private void Awake()
        {
            float bottomPosition = _closePhonePosition.position.y;

            _phone.position = new Vector2(_phone.position.x, bottomPosition);
        }

        private void StartCorutineMove(bool isOpen)
        {
            if (_movePhone != null)
            {
                StopCoroutine(_movePhone);
            }

            _movePhone = StartCoroutine(MovePhone(isOpen));
        }

        private IEnumerator MovePhone(bool isOpen)
        {
            float bottomPosition = _closePhonePosition.position.y;
            float topPosition = _openPhonePosition.position.y;
            float speed = (topPosition - bottomPosition) / _timeSec;

            if (isOpen)
            {
                while (_phone.position.y < topPosition)
                {
                    _phone.position += Vector3.up * speed * Time.deltaTime;
                    yield return null;
                }
            }
            else
            {
                while (_phone.position.y > bottomPosition)
                {
                    _phone.position += Vector3.down * speed * Time.deltaTime;
                    yield return null;
                }
            }
        }

        private void OnEnable() => _openingPhone.PressedPhone += StartCorutineMove;

        private void OnDisable() => _openingPhone.PressedPhone -= StartCorutineMove;
    }
}
