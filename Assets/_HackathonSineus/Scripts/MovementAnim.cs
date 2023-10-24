using UnityEngine;

namespace YagaClub
{
    public class MovementAnim : MonoBehaviour
    {
        [SerializeField] private HorizontalMovement _horizontalMovement;
        [SerializeField] private VerticalMovement _verticalMovement;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Animator _animator;
        private bool _swith = true;
        [SerializeField] private AudioSource _audioSource;
        private bool _isPlay = false;

        private void HorizontalMovementMoved(float direction)
        {
            var xVelocity = _rigidbody.velocity.x;
            _animator.SetFloat("WalkRight", xVelocity);

            if (direction == 0)
            {
                if (_isPlay)
                {
                    _audioSource.Stop();
                    _isPlay = false;
                }
            }
            else if (!_isPlay && _rigidbody.gravityScale != 0)
            {
                _audioSource.Play();
                _isPlay = true;
            }
        }

        private void VerticalMovementMoved()
        {
            if (_rigidbody.gravityScale == 0)
            {
                _animator.SetBool("OnLadder", true);
            }
            else
            {
                _animator.SetBool("OnLadder", false);
            }
        }

        private void PlayerInputInputted(InputData inputData)
        {
            if (inputData.OpenPhone)
            {
                _animator.SetBool("IsPhoneOpen", _swith);
                _swith = !_swith;
            }
        }

        private void OnEnable()
        {
            _horizontalMovement.Moved += HorizontalMovementMoved;
            _verticalMovement.Moved += VerticalMovementMoved;
            _playerInput.Inputted += PlayerInputInputted;
        }

        private void OnDisable()
        {
            _horizontalMovement.Moved -= HorizontalMovementMoved;
            _verticalMovement.Moved -= VerticalMovementMoved;
            _playerInput.Inputted -= PlayerInputInputted;
        }

        /*private void OnValidate()
        {
            _horizontalMovement ??= GetComponent<HorizontalMovement>();
            _verticalMovement ??= GetComponent<VerticalMovement>();
            _playerInput ??= GetComponent<PlayerInput>();
            _rigidbody ??= GetComponent<Rigidbody2D>();
            _animator ??= GetComponent<Animator>();
            _audioSource ??= GetComponent<AudioSource>();
        }*/
    }
}
