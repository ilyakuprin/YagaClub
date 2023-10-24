using UnityEngine;
using Zenject;

namespace YagaClub
{
    public class ChangeOrderScreen : MonoBehaviour
    {
        [SerializeField] private GameObject _activeOrder;
        [SerializeField] private GameObject _noActiveOrder;

        private CreatingOrder _creatingOrder;
        private GiveOrderPoint _giveOrderPoint;

        [Inject]
        private void Constructor(CreatingOrder creatingOrder,
                                 GiveOrderPoint giveOrderPoint)
        {
            _creatingOrder = creatingOrder;
            _giveOrderPoint = giveOrderPoint;
        }

        private void Awake() => OnDeactivateOrderWindow();

        private void OnActivateOrderWindow()
            => ChangeOrderWindow(true);

        private void OnDeactivateOrderWindow()
            => ChangeOrderWindow(false);

        private void ChangeOrderWindow(bool value)
        {
            _activeOrder.SetActive(value);
            _noActiveOrder.SetActive(!value);
        }

        private void OnEnable()
        {
            _creatingOrder.OrderCreated += OnActivateOrderWindow;
            _giveOrderPoint.GetGiveOrderTimer.TimerIsOver += OnDeactivateOrderWindow;
        }

        private void OnDisable()
        {
            _creatingOrder.OrderCreated -= OnActivateOrderWindow;
            _giveOrderPoint.GetGiveOrderTimer.TimerIsOver -= OnDeactivateOrderWindow;
        }
    }
}
