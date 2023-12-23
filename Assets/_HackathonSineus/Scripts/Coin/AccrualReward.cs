using System;
using Zenject;
using YG;

namespace YagaClub
{
    public class AccrualReward : IInitializable, IDisposable
    {
        public event Action RewardAdded;

        private readonly GiveOrderPoint _giveOrderPoint;
        private readonly RewardCompletingOrder _rewardCompletingOrder;
        private readonly Saving _saving;

        public AccrualReward(GiveOrderPoint giveOrderPoint,
                             RewardCompletingOrder rewardCompletingOrder,
                             Saving saving)
        {
            _giveOrderPoint = giveOrderPoint;
            _rewardCompletingOrder = rewardCompletingOrder;
            _saving = saving;
        }

        public void Initialize()
            => _giveOrderPoint.GetGiveOrderTimer.TimerIsOver += OnAddCoin;

        private void OnAddCoin()
        {
            YandexGame.savesData.Money += _rewardCompletingOrder.GetReward;
            _saving.OnSave();
            RewardAdded?.Invoke();
        }

        public void Dispose()
            => _giveOrderPoint.GetGiveOrderTimer.TimerIsOver -= OnAddCoin;
    }
}
