using Zenject;

namespace YagaClub
{
    public class MainMenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Saving>().AsSingle();
        }
    }
}
