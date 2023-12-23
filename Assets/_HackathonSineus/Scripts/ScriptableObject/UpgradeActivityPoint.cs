using System;
using UnityEngine;

namespace YagaClub
{
    [Serializable]
    public struct UpgradeActivityPoint
    {
        //��� ������ ����������� ����� ���������, ��� ������� ������������� ���
        [Min(0)] public float ValueUpgrade;

        //<0 = ��� �����������
        [Min(-1)] public int NumberUpgrades;

        [Min(1)] public int Cost;
    }
}
