using System;
using YG;
using Zenject;

namespace YagaClub
{
    public class UpControlPoint : IInitializable, IDisposable
    {
        private readonly PestControlPoint[] _pestControlPoint;
        private readonly CookingsObjectConfig _config;
        private readonly UpgradesConfig _upgradesConfig;
        private readonly UpgradeButton _upgradeButtons;
        private readonly Saving _saving;

        private readonly float _minTime = 0.1f;

        public UpControlPoint(PestControlPoint[] pestControlPoint,
                              CookingsObjectConfig config,
                              UpgradesConfig upgradesConfig,
                              UpgradeButton upgradeButtons,
                              Saving saving)
        {
            _pestControlPoint = pestControlPoint;
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
                    YandexGame.savesData.LvlCat++;
                    YandexGame.savesData.Money -= _upgradesConfig.Cat.Cost;
                    _saving.OnSave();
                }
                float valueUpgrade = YandexGame.savesData.LvlCat * _upgradesConfig.Cat.ValueUpgrade;
                SetNewTime(cookingObject, valueUpgrade);
            }
            else if (cookingObject == CookingObjects.Boiler)
            {
                if (isBuy)
                {
                    YandexGame.savesData.LvlAerosol++;
                    YandexGame.savesData.Money -= _upgradesConfig.Aerosol.Cost;
                    _saving.OnSave();
                }
                float valueUpgrade = YandexGame.savesData.LvlAerosol * _upgradesConfig.Aerosol.ValueUpgrade;
                SetNewTime(cookingObject, valueUpgrade);
            }
            else if (cookingObject == CookingObjects.CoffeeMachine)
            {
                if (isBuy)
                {
                    YandexGame.savesData.LvlBroom++;
                    YandexGame.savesData.Money -= _upgradesConfig.Broom.Cost;
                    _saving.OnSave();
                }
                float valueUpgrade = YandexGame.savesData.LvlBroom * _upgradesConfig.Broom.ValueUpgrade;
                SetNewTime(cookingObject, valueUpgrade);
            }
        }

        private void SetNewTime(CookingObjects cookingObject, float valueUpgrade)
        {
            ObjectForCoockingConfig coockingPoint = _config.GetObject(cookingObject);
            float time = coockingPoint.TimeActivation - valueUpgrade;

            foreach (var obj in _pestControlPoint)
            {
                if (obj.GetIntCookingObj == (int)cookingObject)
                {
                    if (time < _minTime)
                        time = _minTime;

                    obj.GetTimer.SetTime(time);

                    break;
                }
            }
        }

        public void Initialize()
        {
            foreach (UpgradeMainPointButton ub in _upgradeButtons.Pest)
            {
                ub.PointActivity.BuyButton.onClick.AddListener(() => Up(ub.CookingObject, true));

                Up(ub.CookingObject, false);
            }
        }

        public void Dispose()
        {
            foreach (UpgradeMainPointButton ub in _upgradeButtons.Pest)
            {
                ub.PointActivity.BuyButton.onClick.RemoveListener(() => Up(ub.CookingObject, true));
            }
        }
    }
}
