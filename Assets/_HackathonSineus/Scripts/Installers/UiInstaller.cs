using Zenject;
using UnityEngine;

namespace YagaClub
{
    public class UiInstaller : MonoInstaller
    {
        [SerializeField] private GameMenu _gameMenu;

        public override void InstallBindings()
        {
            Container.Bind<GameMenu>().FromInstance(_gameMenu).AsSingle();
        }
    }
}
