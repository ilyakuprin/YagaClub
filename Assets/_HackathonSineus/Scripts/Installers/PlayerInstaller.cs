using UnityEngine;
using Zenject;

namespace YagaClub
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private Rigidbody2D _rigidbody;

        public override void InstallBindings()
        {
            BindSerializeField();
            BindInterfaces();
            AssignInitializationOrder();
        }

        private void BindSerializeField()
        {
            Container.Bind<PlayerConfig>().FromInstance(_playerConfig).AsSingle();
            Container.Bind<Rigidbody2D>().FromInstance(_rigidbody).AsSingle();
        }

        private void BindInterfaces()
        {
            Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle();
            Container.BindInterfacesAndSelfTo<HorizontalMovement>().AsSingle();
            Container.BindInterfacesAndSelfTo<VerticalMovement>().AsSingle();
            Container.BindInterfacesAndSelfTo<InteractionWithActivationPoint>().AsSingle();
            Container.BindInterfacesAndSelfTo<AcceptanceOrder>().AsSingle();

            Container.BindInterfacesAndSelfTo<OpeningPhone>().AsSingle();
        }

        private void AssignInitializationOrder()
        {
            Container.BindInitializableExecutionOrder<HorizontalMovement>(-20);
            Container.BindInitializableExecutionOrder<VerticalMovement>(-19);
            Container.BindInitializableExecutionOrder<InteractionWithActivationPoint>(-18);
            Container.BindInitializableExecutionOrder<AcceptanceOrder>(-17);

            Container.BindInitializableExecutionOrder<OpeningPhone>(0);
        }
    }
}
