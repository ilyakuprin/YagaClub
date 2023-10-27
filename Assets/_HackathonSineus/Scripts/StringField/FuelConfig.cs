using UnityEngine;

namespace YagaClub
{
    [CreateAssetMenu(fileName = "FuelConfig", menuName = "Configs/FuelConfig")]
    public class FuelConfig : ActivationPointConfig
    {
        [field: SerializeField, Range(1, 500)] public float TotalFuelConsumptionTime { get; private set; }
        [field: SerializeField, Range(0, 500)] public float ExtraFuelTime { get; private set; }
        [field: SerializeField, Range(0, 500)] public float TimeIntervalReceivingFuel { get; private set; }

        private void OnValidate()
        {
            if (ExtraFuelTime > TotalFuelConsumptionTime)
                ExtraFuelTime = TotalFuelConsumptionTime;
        }
    }
}
