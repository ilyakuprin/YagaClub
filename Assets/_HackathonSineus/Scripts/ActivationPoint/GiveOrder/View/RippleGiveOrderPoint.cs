using Zenject;

namespace YagaClub
{
    public class RippleGiveOrderPoint : RipplePoint
    {
        private ColliderHandlerGiveOrder _colliderHandler;

        [Inject]
        public void Construct(ColliderHandlerGiveOrder colliderHandler)
            => _colliderHandler = colliderHandler;

        private void OnEnable()
            => _colliderHandler.ColliderActivated += CallTrigger;

        private void OnDisable()
            => _colliderHandler.ColliderActivated -= CallTrigger;
    }
}
