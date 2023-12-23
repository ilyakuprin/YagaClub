using UnityEngine;
using Zenject;

namespace YagaClub
{
    public class TriggerForVerticalMovement : MonoBehaviour
    {
        private VerticalMovement _verticalMovement;
        private bool _isLadder;
        private readonly TagsField _tagsField = new TagsField();

        [Inject]
        private void Constructor(VerticalMovement verticalMovement)
            => _verticalMovement = verticalMovement;

        private void OnTriggerEnter2D(Collider2D collision)
            => EnterTrigger(collision);

        private void OnTriggerExit2D(Collider2D collision)
            => ExitTrigger(collision);

        private void EnterTrigger(Collider2D collision)
        {
            if (collision.CompareTag(_tagsField.GetLadder))
            {
                _isLadder = true;
                _verticalMovement.OffGravity();
            }
        }

        private void ExitTrigger(Collider2D collision)
        {
            if (collision.CompareTag(_tagsField.GetLadder) && _isLadder)
            {
                _isLadder = false;
                _verticalMovement.OnGravity();
            }
        }
    }
}
