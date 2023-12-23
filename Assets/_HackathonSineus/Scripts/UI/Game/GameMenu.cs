using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace YagaClub
{
    public class GameMenu : MonoBehaviour, IPlayerAction
    {
        [SerializeField] private AudioSource[] _audioSource;
        [SerializeField] private GameObject _menu;
        private PlayerInput _playerInput;
        private bool _activeMenu;
        private readonly int _mainMenuIndex = 0;

        [Inject]
        private void Constructor(PlayerInput playerInput)
            => _playerInput = playerInput;

        private void Awake()
        {
            if (Time.timeScale != 1)
                Time.timeScale = 1;
        }

        public void Executive(InputData inputData)
        {
            if (inputData.Cancel)
                OnResume();
        }

        public void OnResume()
        {
            _menu.SetActive(!_activeMenu);
            _activeMenu = !_activeMenu;

            if (_activeMenu)
            {
                Time.timeScale = 0;
                for (int i = 0; i < _audioSource.Length; i++)
                {
                    _audioSource[i].Pause();
                }
            }
            else
            {
                Time.timeScale = 1;
                for (int i = 0; i < _audioSource.Length; i++)
                {
                    _audioSource[i].Play();
                }
            }
        }

        public void OnExit()
            => SceneManager.LoadScene(_mainMenuIndex);

        private void OnEnable()
            => _playerInput.Inputted += Executive;

        private void OnDisable()
            => _playerInput.Inputted -= Executive;
    }
}
