using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace YagaClub
{
    public class ButtonPause : MonoBehaviour
    {
        [SerializeField] private Button _button;
        private GameMenu _gameMenu;

        [Inject]
        private void Constructor(GameMenu gameMenu)
            => _gameMenu = gameMenu;

        private void OnEnable()
            => _button.onClick.AddListener(() => _gameMenu.OnResume());

        private void OnDisable()
            => _button.onClick.RemoveListener(() => _gameMenu.OnResume());
    }
}
