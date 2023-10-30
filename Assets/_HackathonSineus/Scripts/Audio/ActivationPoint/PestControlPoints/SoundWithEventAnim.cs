using UnityEngine;

namespace YagaClub
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundWithEventAnim : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        private void OnEventAnimationPlaySound()
            => _audioSource.Play();

        private void OnValidate()
            => _audioSource ??= GetComponent<AudioSource>();
    }
}
