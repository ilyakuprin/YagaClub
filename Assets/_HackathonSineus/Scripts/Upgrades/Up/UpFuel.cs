using System;
using YG;
using Zenject;

namespace YagaClub
{
    public class UpFuel : IInitializable, IDisposable
    {
        private readonly FuelHandler _fuelHandler;
        private readonly UpgradesConfig _upgradesConfig;
        private readonly UpgradeButton _upgradeButtons;
        private readonly Saving _saving;

        public UpFuel(FuelHandler fuelHandler,
                      UpgradesConfig upgradesConfig,
                      UpgradeButton upgradeButtons,
                      Saving saving)
        {
            _fuelHandler = fuelHandler;
            _upgradesConfig = upgradesConfig;
            _upgradeButtons = upgradeButtons;
            _saving = saving;
        }

        private void Up(bool isBuy)
        {
            if (isBuy)
            {
                YandexGame.savesData.LvlFuel++;
                YandexGame.savesData.Money -= _upgradesConfig.Fuel.Cost;
                _saving.OnSave();
            }

            float valueUp = YandexGame.savesData.LvlFuel * _upgradesConfig.Fuel.ValueUpgrade;
            _fuelHandler.AddTotalFuel(valueUp);
        }

        public void Initialize()
        {
            _upgradeButtons.Fuel.BuyButton.onClick.AddListener(() => Up(true));
            Up(false);
        }

        public void Dispose()
        {
            _upgradeButtons.Fuel.BuyButton.onClick.RemoveListener(() => Up(true));
        }
    }
}
