using UnityEngine;

namespace YagaClub
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPestControlWithoutAnim : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private PestControlPoint _point;

        private void OnPlaySound()
            => _audioSource.Play();

        private void OnEnable()
            => _point.GetNoCooldpwnTimer.TimerIsOver += OnPlaySound;

        private void OnDisable()
            => _point.GetNoCooldpwnTimer.TimerIsOver -= OnPlaySound;

        private void OnValidate()
            => _audioSource ??= GetComponent<AudioSource>();
    }
}
