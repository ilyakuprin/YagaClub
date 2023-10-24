using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace YagaClub
{
    public class OrderTimerView : MonoBehaviour
    {
        [SerializeField] private Text _timer;
        private OrderTimer _orderTimer;

        [Inject]
        private void Constructor(OrderTimer orderTimer)
            => _orderTimer = orderTimer;

        private void OnViewTime(float time)
            => _timer.text = System.Math.Ceiling(time).ToString();

        private void OnEnable()
            => _orderTimer.GetTimer.HasBeenUpdated += OnViewTime;

        private void OnDisable()
            => _orderTimer.GetTimer.HasBeenUpdated -= OnViewTime;
    }
}
