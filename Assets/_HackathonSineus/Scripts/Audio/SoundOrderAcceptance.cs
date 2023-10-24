using UnityEngine;
using Zenject;

namespace YagaClub
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundOrderAcceptance : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        private AcceptanceOrder _acceptanceOrder;

        [Inject]
        private void Construct(AcceptanceOrder acceptanceOrder)
            => _acceptanceOrder = acceptanceOrder;

        private void OnPlayAudio() => _audioSource.Play();

        private void OnEnable()
            => _acceptanceOrder.OrderAccepted += OnPlayAudio;

        private void OnDisable()
            => _acceptanceOrder.OrderAccepted -= OnPlayAudio;

        private void OnValidate()
            => _audioSource ??= GetComponent<AudioSource>();
    }
}
