using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace YagaClub
{
    [RequireComponent(typeof(Button))]
    public class AcceptOrderButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        private AcceptanceOrder _acceptanceOrder;

        [Inject]
        private void Construct(AcceptanceOrder acceptanceOrder)
            => _acceptanceOrder = acceptanceOrder;

        private void OnInvokeOrderAccepted() => _acceptanceOrder.InvokeOrderAccepted();

        private void OnEnable() => _button.onClick.AddListener(OnInvokeOrderAccepted);

        private void OnDisable() => _button.onClick.RemoveListener(OnInvokeOrderAccepted);

        private void OnValidate() => _button ??= GetComponent<Button>();
    }
}
