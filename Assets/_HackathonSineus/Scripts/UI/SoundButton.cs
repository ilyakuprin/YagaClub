using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using YG;
using Zenject;

namespace YagaClub
{
    public class SoundButton : MonoBehaviour
    {
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private Button _on;
        [SerializeField] private Button _off;
        private readonly string _masterVolume = "MasterVolume";
        private readonly float _minVolume = -80f;
        private readonly float _maxVolume = 0f;
        private Saving _saving;

        [Inject]
        private void Construct(Saving saving)
            => _saving = saving;

        private void OnSetVolume(float volume, bool isSave)
        {
            if (volume >= _maxVolume)
            {
                volume = _maxVolume;
                SetMute(false);
            }
            else if (volume <= _minVolume)
            {
                volume = _minVolume;
                SetMute(true);
            }

            _audioMixer.SetFloat(_masterVolume, volume);

            if (isSave)
            {
                YandexGame.savesData.Volume = volume;
                _saving.OnSave();
            }
        }

        private void SetMute(bool isMute)
        {
            _on.gameObject.SetActive(!isMute);
            _off.gameObject.SetActive(isMute);
        }

        private void OnLoadSave()
            => OnSetVolume(YandexGame.savesData.Volume, false);

        private void OnEnable()
        {
            _on.onClick.AddListener(() => OnSetVolume(_minVolume, true));
            _off.onClick.AddListener(() => OnSetVolume(_maxVolume, true));

            _saving.SaveDataReceived += OnLoadSave;
        }

        private void OnDisable()
        {
            _on.onClick.RemoveListener(() => OnSetVolume(_minVolume, true));
            _off.onClick.RemoveListener(() => OnSetVolume(_maxVolume, true));

            _saving.SaveDataReceived -= OnLoadSave;
        }
    }
}
