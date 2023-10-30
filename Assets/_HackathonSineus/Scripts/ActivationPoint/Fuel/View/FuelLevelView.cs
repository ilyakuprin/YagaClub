using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace YagaClub
{
    public class FuelLevelView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        private FuelHandler _fuelHandler;

        [Inject]
        private void Construct(FuelHandler fuelHandler)
            => _fuelHandler = fuelHandler;

        private void FillImage(float value)
            => _image.fillAmount = value / _fuelHandler.GetTotalValue;

        private void OnEnable()
            => _fuelHandler.GetTimer.HasBeenUpdated += FillImage;

        private void OnDisable()
            => _fuelHandler.GetTimer.HasBeenUpdated -= FillImage;
    }
}
