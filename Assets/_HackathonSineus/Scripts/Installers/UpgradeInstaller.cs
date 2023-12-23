using UnityEngine;
using Zenject;

namespace YagaClub
{
    public class UpgradeInstaller : MonoInstaller
    {
        [SerializeField] private UpgradeButton _upgradeButtons;
        [SerializeField] private UpgradesConfig _upgradesConfig;

        public override void InstallBindings()
        {
            BindSerializeField();
            BindInterfaces();
        }

        private void BindSerializeField()
        {
            Container.Bind<UpgradesConfig>().FromInstance(_upgradesConfig).AsSingle();
            Container.Bind<UpgradeButton>().FromInstance(_upgradeButtons).AsSingle();
        }

        private void BindInterfaces()
        {
            Container.BindInterfacesAndSelfTo<UpCookingPoint>().AsSingle();
            Container.BindInterfacesAndSelfTo<UpControlPoint>().AsSingle();
            Container.BindInterfacesAndSelfTo<UpFuel>().AsSingle();
            Container.BindInterfacesAndSelfTo<ActivatingShoppingButtons>().AsSingle();
            Container.BindInterfacesAndSelfTo<Saving>().AsSingle();
        }
    }
}
