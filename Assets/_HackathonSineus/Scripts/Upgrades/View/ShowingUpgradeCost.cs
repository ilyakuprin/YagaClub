using UnityEngine;
using UnityEngine.UI;
using YG;
using Zenject;

namespace YagaClub
{
    public class ShowingUpgradeCost : MonoBehaviour
    {
        [SerializeField] private Text _stove;
        [SerializeField] private Text _boiler;
        [SerializeField] private Text _coffeeMachine;
        [SerializeField] private Text _aerosol;
        [SerializeField] private Text _broom;
        [SerializeField] private Text _cat;
        [SerializeField] private Text _fuel;

        private UpgradesConfig _config;

        [Inject]
        private void Construct(UpgradesConfig config)
            => _config = config;

        private void OnShow()
        {
            _stove.text = _config.Stove.Cost.ToString();
            _boiler.text = _config.Boiler.Cost.ToString();
            _coffeeMachine.text = _config.CoffeeMachine.Cost.ToString();
            _aerosol.text = _config.Aerosol.Cost.ToString();
            _broom.text = _config.Broom.Cost.ToString();
            _cat.text = _config.Cat.Cost.ToString();
            _fuel.text = _config.Fuel.Cost.ToString();
        }

        private void OnEnable()
            => YandexGame.GetDataEvent += OnShow;

        private void OnDisable()
            => YandexGame.GetDataEvent -= OnShow;
    }
}
