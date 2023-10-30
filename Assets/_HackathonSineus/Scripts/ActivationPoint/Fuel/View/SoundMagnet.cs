using UnityEngine;
using Zenject;

namespace YagaClub
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundMagnet : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _magnetize;
        [SerializeField] private AudioClip _unmagnetize;
        private GettingFuelPoint _point;

        [Inject]
        private void Construct(GettingFuelPoint point)
            => _point = point;

        private void OnPlaySound(bool value)
        {
            if (value)
                _audioSource.clip = _magnetize;
            else
                _audioSource.clip = _unmagnetize;

            _audioSource.Play();
        }

        private void OnEnable()
            => _point.TookFuel += OnPlaySound;

        private void OnDisable()
            => _point.TookFuel -= OnPlaySound;

        private void OnValidate()
            => _audioSource ??= GetComponent<AudioSource>();
    }
}
