using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

namespace YagaClub
{
    public class ButtonMainMenu : MonoBehaviour
    {
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _trainingButton;
        [SerializeField] private GameObject _education;
        private readonly int _gameSceneIndex = 2;
        private readonly int _educationSceneIndex = 1;

        private void OnStartGame()
        {
            if (YandexGame.savesData.IsTraining—ompleted)
                SceneManager.LoadScene(_gameSceneIndex);
            else
                OnStartTraining();
        }

        private void OnStartTraining()
        {
            SceneManager.LoadScene(_educationSceneIndex);
        }

        private void OnEnable()
        {
            _startGameButton.onClick.AddListener(OnStartGame);

            if (YandexGame.savesData.IsTraining—ompleted)
                _education.SetActive(true);

            _trainingButton.onClick.AddListener(OnStartTraining);
        }

        private void OnDisable()
        {
            _startGameButton.onClick.RemoveListener(OnStartGame);
            _trainingButton.onClick.RemoveListener(OnStartTraining);
        }
    }
}
