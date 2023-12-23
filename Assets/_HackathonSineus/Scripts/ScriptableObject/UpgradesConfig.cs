using UnityEngine;

namespace YagaClub
{
    [CreateAssetMenu(fileName = "UpgradesConfig", menuName = "Configs/UpgradesConfig")]
    public class UpgradesConfig : ScriptableObject
    {
        public UpgradeActivityPoint Stove;
        public UpgradeActivityPoint Boiler;
        public UpgradeActivityPoint CoffeeMachine;
        public UpgradeActivityPoint Aerosol;
        public UpgradeActivityPoint Broom;
        public UpgradeActivityPoint Cat;
        public UpgradeActivityPoint Fuel;
    }
}
