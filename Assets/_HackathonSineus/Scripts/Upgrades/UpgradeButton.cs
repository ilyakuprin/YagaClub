using System;

namespace YagaClub
{
    [Serializable]
    public struct UpgradeButton
    {
        public UpgradeMainPointButton[] Cooking;
        public UpgradeMainPointButton[] Pest;
        public UpgradePointActivityButton Fuel;
    }
}
