using UnityEngine;
using Zenject;

namespace YagaClub
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundActivityPoint : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private ActivityPoint _activityPoint;
        private InteractionWithActivationPoint _interactionWithAP;
        private bool _isPlay;
        private float _remainigTime;
        private float _updateTime;

        [Inject]
        private void Constructor(InteractionWithActivationPoint interactionWithAP)
            => _interactionWithAP = interactionWithAP;

        private void OnControlSound(bool value)
        {
            if (value && _remainigTime != _updateTime)
            {
                _remainigTime = _updateTime;

                if (!_isPlay)
                    PlayAudio();
            }
            else if (_isPlay)
                StopPlay();
        }

        private void OnSetUpdateTime(float value) => _updateTime = value;

        private void PlayAudio()
        {
            _audioSource.Play();
            _isPlay = true;
        }

        private void StopPlay()
        {
            _audioSource.Stop();
            _isPlay = false;
        }

        private void OnEnable()
        {
            _activityPoint.GetTimer.HasBeenUpdated += OnSetUpdateTime;
            _interactionWithAP.Pressed += OnControlSound;
        }

        private void OnDisable()
        {
            _activityPoint.GetTimer.HasBeenUpdated -= OnSetUpdateTime;
            _interactionWithAP.Pressed -= OnControlSound;
        }

        private void OnValidate()
        {
            _audioSource ??= GetComponent<AudioSource>();
            _activityPoint ??= GetComponent<ActivityPoint>();
        }
    }
}
