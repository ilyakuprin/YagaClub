using System;
using UnityEngine;

namespace YagaClub
{
    [Serializable]
    public struct UpgradeActivityPoint
    {
        //Для бустов уменьшается время активации, для топлива увеличивается бак
        [Min(0)] public float ValueUpgrade;

        //<0 = без ограничений
        [Min(-1)] public int NumberUpgrades;

        [Min(1)] public int Cost;
    }
}
