using System;
using UnityEngine;
using UnityEngine.UI;

namespace YagaClub
{
    [Serializable]
    public struct UpgradePestButton
    {
        [field: SerializeField] public Button BuyButton { get; private set; }
        [field: SerializeField] public Text Cost { get; private set; }
        [field: SerializeField] public CookingObjects CookingObject { get; private set; }
    }
}
