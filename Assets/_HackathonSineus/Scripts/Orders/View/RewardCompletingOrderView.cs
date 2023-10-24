using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace YagaClub
{
    public class RewardCompletingOrderView : MonoBehaviour
    {
        [SerializeField] private Text _reward;
        private RewardCompletingOrder _rewardCompletingOrder;

        [Inject]
        private void Constructor(RewardCompletingOrder rewardCompletingOrder)
            => _rewardCompletingOrder = rewardCompletingOrder;

        private void OnView—ost()
            => _reward.text = _rewardCompletingOrder.GetReward.ToString();

        private void OnEnable()
        {
            _rewardCompletingOrder.RewardCalculated += OnView—ost;
            _rewardCompletingOrder.RewardReduced += OnView—ost;
        }

        private void OnDisable()
        {
            _rewardCompletingOrder.RewardCalculated -= OnView—ost;
            _rewardCompletingOrder.RewardReduced -= OnView—ost;
        }
    }
}
