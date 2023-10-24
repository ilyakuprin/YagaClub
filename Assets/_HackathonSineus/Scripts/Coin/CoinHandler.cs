using System;
using Zenject;

namespace YagaClub
{
    public class CoinHandler : IInitializable, IDisposable
    {
        public event Action RewardAdded;

        private readonly GiveOrderPoint _giveOrderPoint;
        private readonly RewardCompletingOrder _rewardCompletingOrder;
        private int _countCoin;

        public int GetCountCoin { get => _countCoin; }

        public CoinHandler(GiveOrderPoint giveOrderPoint,
                           RewardCompletingOrder rewardCompletingOrder)
        {
            _giveOrderPoint = giveOrderPoint;
            _rewardCompletingOrder = rewardCompletingOrder;
        }

        public void Initialize()
            => _giveOrderPoint.GetGiveOrderTimer.TimerIsOver += OnAddCoin;

        private void OnAddCoin()
        {
            _countCoin += _rewardCompletingOrder.GetReward;
            RewardAdded?.Invoke();
        }

        public void Dispose()
            => _giveOrderPoint.GetGiveOrderTimer.TimerIsOver -= OnAddCoin;
    }
}
