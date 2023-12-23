using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;
using Zenject;

namespace YagaClub
{
    public class Defeat : MonoBehaviour
    {
        [SerializeField] private Button _watchAds;
        [SerializeField] private Button _restart;
        [SerializeField] private Button _exit;

        [SerializeField] private GameObject _pauseButtons;
        [SerializeField] private GameObject _defeatButtons;

        private GameMenu _gameMenu;
        private FuelHandler _fuelHandler;
        private readonly int _mainMenuIndex = 0;
        [SerializeField] private int _valueFuel;

        [Inject]
        private void Construct(GameMenu gameMenu,
                               FuelHandler fuelHandler)
        {
            _gameMenu = gameMenu;
            _fuelHandler = fuelHandler;
        }

        private void Over()
        {
            _gameMenu.enabled = false;
            _gameMenu.OnResume();
            SetButtons(false);
        }

        private void AddFuel(int _)
        {
            _fuelHandler.AddTime(_valueFuel);
            Continue();
        }

        private void Continue()
        {
            _gameMenu.enabled = true;
            _gameMenu.OnResume();
            SetButtons(true);
        }

        private void SetButtons(bool isPause)
        {
            _pauseButtons.SetActive(isPause);
            _defeatButtons.SetActive(!isPause);
        }

        private void Restart()
            => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        public void OnExit()
            => SceneManager.LoadScene(_mainMenuIndex);

        private void OnEnable()
        {
            _fuelHandler.GameOver += Over;

            _exit.onClick.AddListener(OnExit);
            _restart.onClick.AddListener(Restart);
            _watchAds.onClick.AddListener(() => YandexGame.RewVideoShow(0));
            YandexGame.RewardVideoEvent += AddFuel;
        }

        private void OnDisable()
        {
            _fuelHandler.GameOver -= Over;

            _exit.onClick.RemoveListener(OnExit);
            _restart.onClick.RemoveListener(Restart);
            _watchAds.onClick.RemoveListener(() => YandexGame.RewVideoShow(0));
            YandexGame.RewardVideoEvent -= AddFuel;
        }
    }
}
