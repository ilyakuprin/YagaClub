using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

namespace YagaClub
{
    public class NextSceneButton : MonoBehaviour
    {
        [SerializeField] private Button _nextSceneButton;
        private readonly int _gameSceneIndex = 2;

        private void OnNextScene()
        {
            YandexGame.savesData.IsTraining—ompleted = true;
            YandexGame.SaveProgress();

            SceneManager.LoadScene(_gameSceneIndex);
        }

        private void OnEnable()
        {
            _nextSceneButton.onClick.AddListener(OnNextScene);
        }

        private void OnDisable()
        {
            _nextSceneButton.onClick.RemoveListener(OnNextScene);
        }
    }
}
