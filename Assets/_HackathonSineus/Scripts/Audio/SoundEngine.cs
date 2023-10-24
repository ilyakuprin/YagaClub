using System.Collections;
using UnityEngine;

namespace YagaClub
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundEngine : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _start;
        [SerializeField] private AudioClip _work;

        private void Awake() => _audioSource.loop = false;

        private void Start() => StartCoroutine(ReplaceAudioClip());

        private IEnumerator ReplaceAudioClip()
        {
            while (_audioSource.isPlaying || Time.timeScale == 0)
            {
                yield return null;
            }

            _audioSource.clip = _work;
            _audioSource.loop = true;

            _audioSource.Play();
        }

        private void OnValidate()
            => _audioSource ??= GetComponent<AudioSource>();
    }
}
