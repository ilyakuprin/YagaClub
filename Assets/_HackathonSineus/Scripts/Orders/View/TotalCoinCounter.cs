using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace YagaClub
{
    public class TotalCoinCounter : MonoBehaviour
    {
        [SerializeField] private Text _totalCoin;
        private CoinHandler _coinHandler;

        [Inject]
        private void Constructor(CoinHandler coinHandler)
            => _coinHandler = coinHandler;

        private void OnAddCoin()
            => _totalCoin.text = _coinHandler.GetCountCoin.ToString();

        private void OnEnable()
            => _coinHandler.RewardAdded += OnAddCoin;

        private void OnDisable()
            => _coinHandler.RewardAdded -= OnAddCoin;
    }
}
