using UnityEngine;
using Zenject;

namespace YagaClub
{
    public class PlayerAnimationInstaller : MonoInstaller
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private readonly HashingAnimation _hashingAnimation = new HashingAnimation();

        public override void InstallBindings()
        {
            BindSerializeField();
            BindInterfaces();
        }

        private void BindSerializeField()
        {
            Container.Bind<Animator>().FromInstance(_animator).AsSingle();
            Container.Bind<SpriteRenderer>().FromInstance(_spriteRenderer).AsSingle();
            Container.Bind<HashingAnimation>().FromInstance(_hashingAnimation).AsSingle();
        }

        private void BindInterfaces()
        {
            Container.BindInterfacesAndSelfTo<WalkingAnimation>().AsSingle();
            Container.BindInterfacesAndSelfTo<MovementOnStairAnimation>().AsSingle();
            Container.BindInterfacesAndSelfTo<TakePhoneAnimation>().AsSingle();
        }
    }
}
