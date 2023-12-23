using UnityEngine;
using UnityEngine.UI;
using YG;
using Zenject;

namespace YagaClub
{
    public class ShowingUpgradeLvl : MonoBehaviour
    {
        [SerializeField] private Text _stove;
        [SerializeField] private Text _boiler;
        [SerializeField] private Text _coffeeMachine;
        [SerializeField] private Text _aerosol;
        [SerializeField] private Text _broom;
        [SerializeField] private Text _cat;
        [SerializeField] private Text _fuel;

        private Saving _saving;

        [Inject]
        private void Construct(Saving saving)
            => _saving = saving;

        private void OnShow()
        {
            _stove.text = YandexGame.savesData.LvlStove.ToString();
            _boiler.text = YandexGame.savesData.LvlBoiler.ToString();
            _coffeeMachine.text = YandexGame.savesData.LvlCoffeeMachine.ToString();
            _aerosol.text = YandexGame.savesData.LvlAerosol.ToString();
            _broom.text = YandexGame.savesData.LvlBroom.ToString();
            _cat.text = YandexGame.savesData.LvlCat.ToString();
            _fuel.text = YandexGame.savesData.LvlFuel.ToString();
        }

        private void OnEnable()
            => _saving.SaveDataReceived += OnShow;

        private void OnDisable()
            => _saving.SaveDataReceived -= OnShow;
    }
}
