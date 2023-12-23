using System;
using UnityEngine.UI;
using YG;
using Zenject;

namespace YagaClub
{
    public class ActivatingShoppingButtons : IInitializable, IDisposable
    {
        private readonly UpgradeButton _upgradeButtons;
        private readonly UpgradesConfig _upgradesConfig;
        private readonly Saving _saving;

        public ActivatingShoppingButtons(UpgradeButton upgradeButtons,
                                         UpgradesConfig upgradesConfig,
                                         Saving saving)
        {
            _upgradeButtons = upgradeButtons;
            _upgradesConfig = upgradesConfig;
            _saving = saving;
        }

        private void CheckAndActivateButton()
        {
            if (YandexGame.savesData.Money >= _upgradesConfig.Stove.Cost &&
                YandexGame.savesData.LvlStove < _upgradesConfig.Stove.NumberUpgrades)
            {
                ActivateButton(CookingObjects.Stove, _upgradeButtons.Cooking, true);
            }
            else
            {
                ActivateButton(CookingObjects.Stove, _upgradeButtons.Cooking, false);
            }

            if (YandexGame.savesData.Money >= _upgradesConfig.Boiler.Cost &&
                YandexGame.savesData.LvlBoiler < _upgradesConfig.Boiler.NumberUpgrades)
            {
                ActivateButton(CookingObjects.Boiler, _upgradeButtons.Cooking, true);
            }
            else
            {
                ActivateButton(CookingObjects.Boiler, _upgradeButtons.Cooking, false);
            }

            if (YandexGame.savesData.Money >= _upgradesConfig.CoffeeMachine.Cost &&
                YandexGame.savesData.LvlCoffeeMachine < _upgradesConfig.CoffeeMachine.NumberUpgrades)
            {
                ActivateButton(CookingObjects.CoffeeMachine, _upgradeButtons.Cooking, true);
            }
            else
            {
                ActivateButton(CookingObjects.CoffeeMachine, _upgradeButtons.Cooking, false);
            }

            if (YandexGame.savesData.Money >= _upgradesConfig.Cat.Cost &&
                YandexGame.savesData.LvlCat < _upgradesConfig.Cat.NumberUpgrades)
            {
                ActivateButton(CookingObjects.Stove, _upgradeButtons.Pest, true);
            }
            else
            {
                ActivateButton(CookingObjects.Stove, _upgradeButtons.Pest, false);
            }

            if (YandexGame.savesData.Money >= _upgradesConfig.Broom.Cost &&
                YandexGame.savesData.LvlBroom < _upgradesConfig.Broom.NumberUpgrades)
            {
                ActivateButton(CookingObjects.CoffeeMachine, _upgradeButtons.Pest, true);
            }
            else
            {
                ActivateButton(CookingObjects.CoffeeMachine, _upgradeButtons.Pest, false);
            }

            if (YandexGame.savesData.Money >= _upgradesConfig.Aerosol.Cost &&
                YandexGame.savesData.LvlAerosol < _upgradesConfig.Aerosol.NumberUpgrades)
            {
                ActivateButton(CookingObjects.Boiler, _upgradeButtons.Pest, true);
            }
            else
            {
                ActivateButton(CookingObjects.Boiler, _upgradeButtons.Pest, false);
            }

            if (YandexGame.savesData.Money >= _upgradesConfig.Fuel.Cost &&
                YandexGame.savesData.LvlFuel < _upgradesConfig.Fuel.NumberUpgrades)
            {
                SetInteractable(_upgradeButtons.Fuel.BuyButton, true);
            }
            else
            {
                SetInteractable(_upgradeButtons.Fuel.BuyButton, false);
            }
        }

        private void ActivateButton(CookingObjects cookingObject, UpgradeMainPointButton[] _point, bool activate)
        {
            foreach (UpgradeMainPointButton ub in _point)
            {
                if (ub.CookingObject == cookingObject)
                {
                    SetInteractable(ub.PointActivity.BuyButton, activate);
                    break;
                }
            }
        }

        private void SetInteractable(Button button, bool isInteractable)
            => button.interactable = isInteractable;

        public void Initialize()
            => _saving.SaveDataReceived += CheckAndActivateButton;

        public void Dispose()
            => _saving.SaveDataReceived -= CheckAndActivateButton;
    }
}
