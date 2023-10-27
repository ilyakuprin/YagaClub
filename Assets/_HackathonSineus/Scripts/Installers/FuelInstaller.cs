using UnityEngine;
using Zenject;

namespace YagaClub
{
    public class FuelInstaller : MonoInstaller
    {
        [SerializeField] private FuelConfig _fuelConfig;
        [SerializeField] private FuelHandler _fuelHandler;
        [SerializeField] private GettingFuelPoint _gettingFuelPoint;
        
        public override void InstallBindings()
        {
            BindSerializeField();
        }

        private void BindSerializeField()
        {
            Container.Bind<FuelConfig>().FromInstance(_fuelConfig).AsSingle();
            Container.Bind<FuelHandler>().FromInstance(_fuelHandler).AsSingle();
            Container.Bind<GettingFuelPoint>().FromInstance(_gettingFuelPoint).AsSingle();
        }
    }
}
