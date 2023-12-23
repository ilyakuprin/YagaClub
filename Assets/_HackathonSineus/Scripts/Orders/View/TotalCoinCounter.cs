using UnityEngine;
using UnityEngine.UI;
using Zenject;
using YG;

namespace YagaClub
{
    public class TotalCoinCounter : MonoBehaviour
    {
        [SerializeField] private Text _totalCoin;
        private Saving _saving;

        [Inject]
        private void Constructor(Saving saving)
            => _saving = saving;

        private void OnChangeCoin()
            => _totalCoin.text = YandexGame.savesData.Money.ToString();

        private void OnEnable()
            => _saving.SaveDataReceived += OnChangeCoin;

        private void OnDisable()
            => _saving.SaveDataReceived -= OnChangeCoin;
    }
}
