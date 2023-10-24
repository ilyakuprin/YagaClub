using UnityEngine;
using Zenject;

namespace YagaClub
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundOrderReceipt : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        private GiveOrderPoint _giveOrderPoint;

        [Inject]
        private void Constructor(GiveOrderPoint giveOrderPoint)
            => _giveOrderPoint = giveOrderPoint;

        private void Start() => OnPlayAudio();

        private void OnPlayAudio() => _audioSource.Play();

        private void OnEnable()
            => _giveOrderPoint.GetGiveOrderTimer.TimerIsOver += OnPlayAudio;

        private void OnDisable()
            => _giveOrderPoint.GetGiveOrderTimer.TimerIsOver -= OnPlayAudio;

        private void OnValidate()
            => _audioSource ??= GetComponent<AudioSource>();
    }
}
