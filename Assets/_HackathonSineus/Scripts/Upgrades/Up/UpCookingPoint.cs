using System;
using YG;
using Zenject;

namespace YagaClub
{
    public class UpCookingPoint : IInitializable, IDisposable
    {
        private readonly CookingPoint[] _cookingPoints;
        private readonly CookingsObjectConfig _config;
        private readonly UpgradesConfig _upgradesConfig;
        private readonly UpgradeButton _upgradeButtons;
        private readonly Saving _saving;

        private readonly float _minTime = 0.1f;

        public UpCookingPoint(CookingPoint[] cookingPoints,
                              CookingsObjectConfig config,
                              UpgradesConfig upgradesConfig,
                              UpgradeButton upgradeButtons,
                              Saving saving)
        {
            _cookingPoints = cookingPoints;
            _config = config;
            _upgradesConfig = upgradesConfig;
            _upgradeButtons = upgradeButtons;
            _saving = saving;
        }

        private void Up(CookingObjects cookingObject, bool isBuy)
        {
            if (cookingObject == CookingObjects.Stove)
            {
                if (isBuy)
                {
                    YandexGame.savesData.LvlStove++;
                    YandexGame.savesData.Money -= _upgradesConfig.Stove.Cost;
                    _saving.OnSave();
                }
                float valueUpgrade = YandexGame.savesData.LvlStove * _upgradesConfig.Stove.ValueUpgrade;
                SetNewTime(cookingObject, valueUpgrade);
            }
            else if (cookingObject == CookingObjects.Boiler)
            {
                if (isBuy)
                {
                    YandexGame.savesData.LvlBoiler++;
                    YandexGame.savesData.Money -= _upgradesConfig.Boiler.Cost;
                    _saving.OnSave();
                }
                float valueUpgrade = YandexGame.savesData.LvlBoiler * _upgradesConfig.Boiler.ValueUpgrade;
                SetNewTime(cookingObject, valueUpgrade);
            }
            else if (cookingObject == CookingObjects.CoffeeMachine)
            {
                if (isBuy)
                {
                    YandexGame.savesData.LvlCoffeeMachine++;
                    YandexGame.savesData.Money -= _upgradesConfig.CoffeeMachine.Cost;
                    _saving.OnSave();
                }
                float valueUpgrade = YandexGame.savesData.LvlCoffeeMachine * _upgradesConfig.CoffeeMachine.ValueUpgrade;
                SetNewTime(cookingObject, valueUpgrade);
            }
        }

        private void SetNewTime(CookingObjects cookingObject, float valueUpgrade)
        {
            ObjectForCoockingConfig coockingPoint = _config.GetObject(cookingObject);
            float time = coockingPoint.TimeActivation - valueUpgrade;

            foreach (var obj in _cookingPoints)
            {
                if (obj.GetIntCookingObj == (int)cookingObject)
                {
                    if (time < _minTime)
                        time = _minTime;

                    obj.GetCookingTimer.SetTime(time);
                    break;
                }
            }
        }

        public void Initialize()
        {
            foreach (UpgradeMainPointButton ub in _upgradeButtons.Cooking)
            {
                ub.PointActivity.BuyButton.onClick.AddListener(() => Up(ub.CookingObject, true));

                Up(ub.CookingObject, false);
            }
        }

        public void Dispose()
        {
            foreach (UpgradeMainPointButton ub in _upgradeButtons.Cooking)
            {
                ub.PointActivity.BuyButton.onClick.RemoveListener(() => Up(ub.CookingObject, true));
            }
        }
    }
}
