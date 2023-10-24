using System.Collections;
using UnityEngine;
using Zenject;

namespace YagaClub
{
    public class LiftingPhone : MonoBehaviour
    {
        [SerializeField] private RectTransform _phone;
        [SerializeField] private OpeningPhone _openingPhone;
        [SerializeField] private float _bottomPosition;
        [SerializeField] private float _topPosition;
        [SerializeField] private float _speed;

        private Coroutine _movePhone;

        [Inject]
        private void Constructor(OpeningPhone openingPhone)
            => _openingPhone = openingPhone;

        private void Awake() 
            => _phone.position = new Vector2(_phone.position.x, _bottomPosition);

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
            if (isOpen)
            {
                while (_phone.position.y < _topPosition)
                {
                    _phone.position += Vector3.up * _speed * Time.deltaTime;
                    yield return null;
                }
            }
            else
            {
                while (_phone.position.y > _bottomPosition)
                {
                    _phone.position += Vector3.down * _speed * Time.deltaTime;
                    yield return null;
                }
            }
        }

        private void OnEnable() => _openingPhone.PressedPhone += StartCorutineMove;

        private void OnDisable() => _openingPhone.PressedPhone -= StartCorutineMove;
    }
}
