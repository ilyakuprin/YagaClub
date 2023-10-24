using UnityEngine;
using Zenject;

namespace YagaClub
{
    public class TriggerForActivationPoint : MonoBehaviour
    {
        private InteractionWithActivationPoint _interactionWithAP;
        private bool _inTriggerPoint;
        private ActivityPoint _activityPoint;

        [Inject]
        private void Constructor(InteractionWithActivationPoint interactionWithAP)
            => _interactionWithAP = interactionWithAP;

        private void OnTriggerEnter2D(Collider2D collision) => EnterTrigger(collision);

        private void OnTriggerExit2D(Collider2D collision) => ExitTrigger(collision);

        private void EnterTrigger(Collider2D collision)
        {
            if (collision.TryGetComponent(out ActivityPoint activityPoint))
            {
                _activityPoint = activityPoint;
                _interactionWithAP.SetActivityPoint(activityPoint);
                _inTriggerPoint = true;
            }
        }

        private void ExitTrigger(Collider2D collision)
        {
            if (collision.TryGetComponent(out ActivityPoint activityPoint) && _inTriggerPoint)
            {
                if (activityPoint == _activityPoint)
                {
                    _interactionWithAP.ForgetActivityPoint();
                    _inTriggerPoint = false;
                    _activityPoint = null;
                }
            }
        }
    }
}
