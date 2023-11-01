using System.Collections;
using UnityEngine;

namespace YagaClub
{
    public class MovingBackground : MonoBehaviour
    {
        [SerializeField, Range(0, 50)] private float _speed;
        private float _accelerationModifier = 1;
        private float _length;
        private Vector3 _startPosition;
        private Coroutine _move;

        public float GetMaxAcceleration { get => 1; }

        public void SetModifier(float value)
        {
            if (value < 0)
                _accelerationModifier = 0;
            else if (value > GetMaxAcceleration)
                _accelerationModifier = GetMaxAcceleration;
            else
                _accelerationModifier = value;
        }

        private void Awake()
        {
            _length = GetComponent<SpriteRenderer>().bounds.size.x;
            _startPosition = transform.position;
        }

        private void Start()
            => StartMove();

        private IEnumerator Move()
        {
            while (true)
            {
                while (transform.position.x > -_length)
                {
                    float positionX = transform.position.x - _speed * _accelerationModifier * Time.deltaTime;
                    transform.position = new Vector3(positionX, transform.position.y, transform.position.z);
                    yield return null;
                }

                transform.position = _startPosition;
            }
        }

        private void StartMove()
        {
            _move = StartCoroutine(Move());
        }

        private void StopMove()
        {
            if (_move != null)
                StopCoroutine(_move);
        }
    }
}
