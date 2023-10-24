using UnityEngine;
using Zenject;

namespace YagaClub
{
    public class PathOnLineView : MonoBehaviour
    {
        [SerializeField] private RectTransform _pathLine;
        [SerializeField] private RectTransform _hutIcon;
        private OrderTimer _orderTimer;
        private CalculationTimeOrder _calculationTime;
        private Vector3 _startPosition;
        private float _speed;
        private float _length;

        [Inject]
        private void Constructor(OrderTimer orderTimer,
                                 CalculationTimeOrder calculationTime)
        {
            _orderTimer = orderTimer;
            _calculationTime = calculationTime;
        }

        private void Awake()
        {
            _length = _pathLine.sizeDelta.x;
            _startPosition = new Vector2(-_length / 2, _hutIcon.localPosition.y);
            SetHutStartPosition();
        }

        public void OnCalculateSpeed()
        {
            _speed = _length / _calculationTime.GetTime;
            SetHutStartPosition();
        }

        private void SetHutStartPosition()
            => _hutIcon.localPosition = _startPosition;

        public void OnViewPath(float time)
            => _hutIcon.localPosition = _startPosition + Vector3.right * (_length - time * _speed);

        private void OnEnable()
        {
            _calculationTime.TimeCalculated += OnCalculateSpeed;
            _orderTimer.GetTimer.HasBeenUpdated += OnViewPath;
        }

        private void OnDisable()
        {
            _calculationTime.TimeCalculated -= OnCalculateSpeed;
            _orderTimer.GetTimer.HasBeenUpdated -= OnViewPath;
        }
    }
}
