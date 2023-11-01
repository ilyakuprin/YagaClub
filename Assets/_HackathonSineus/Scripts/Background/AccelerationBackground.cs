using System.Collections;
using UnityEngine;

namespace YagaClub
{
    public class AccelerationBackground : MonoBehaviour
    {
        [SerializeField] private MovingBackground[] _moving;
        [SerializeField] private float _maxTimeAcceleration;
        private Coroutine _accelerate;

        private void Start()
            => StartAccelerate();

        private IEnumerator Accelerate()
        {
            float currentTime = 0;
            float currentAcceleration = 0;
            float maxAcceleration = _moving[0].GetMaxAcceleration;
            float speed = maxAcceleration / _maxTimeAcceleration;

            while (currentTime < _maxTimeAcceleration)
            {
                currentAcceleration += speed * Time.deltaTime;

                for (int i = 0; i < _moving.Length; i++)
                {
                    _moving[i].SetModifier(currentAcceleration);
                }

                currentTime += Time.deltaTime;

                yield return null;
            }
        }

        private void StartAccelerate()
        {
            _accelerate = StartCoroutine(Accelerate());
        }

        private void StopAccelerate()
        {
            if (_accelerate != null)
                StopCoroutine(_accelerate);
        }
    }
}
