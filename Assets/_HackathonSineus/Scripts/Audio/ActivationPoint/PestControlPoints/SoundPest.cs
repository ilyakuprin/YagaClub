using UnityEngine;

namespace YagaClub
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPest : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private PestControlPoint _point;

        private void OnPlaySound()
            => _audioSource.Play();

        private void OnPlaySound(bool value)
        {
            if (value)
                OnPlaySound();
        }

        private void OnEnable()
        {
            _point.GetNoCooldpwnTimer.TimerIsOver += OnPlaySound;
            _point.ColliderActivated += OnPlaySound;
        }

        private void OnDisable()
        {
            _point.GetNoCooldpwnTimer.TimerIsOver -= OnPlaySound;
            _point.ColliderActivated += OnPlaySound;
        }

        private void OnValidate()
            => _audioSource ??= GetComponent<AudioSource>();
    }
}
