using UnityEngine;
using UnityEngine.UI;

namespace YagaClub
{
    public class TimeBar : MonoBehaviour
    {
        [SerializeField] private Image _filler;
        private CoroutineTimer _timer;

        public void Initialize(CoroutineTimer timer) => _timer = timer;

        private void OnValueChanged(float value)
            => _filler.fillAmount = value;

        private void OnEnable()
        {
            if (_timer != null)
                _timer.HasBeenUpdated += OnValueChanged;
        }

        private void OnDisable()
        {
            if (_timer != null)
                _timer.HasBeenUpdated -= OnValueChanged;
        }
    }
}
