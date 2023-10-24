using UnityEngine;
using Zenject;

namespace YagaClub
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPhoneOnOff : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        private OpeningPhone _openingPhone;

        [Inject]
        private void Constructor(OpeningPhone openingPhone)
            => _openingPhone = openingPhone;

        private void PlayAudio(bool _) => _audioSource.Play();

        private void OnEnable()
            => _openingPhone.PressedPhone += PlayAudio;

        private void OnDisable()
            => _openingPhone.PressedPhone -= PlayAudio;

        private void OnValidate()
            => _audioSource ??= GetComponent<AudioSource>();
    }
}
