using UnityEngine;
using Zenject;

namespace YagaClub
{
    public class OrderInstaller : MonoInstaller
    {
        [SerializeField] private CookingsObjectConfig _cookingsObject;
        [SerializeField] private ActivationPointConfig _giveOrderConfig;
        [SerializeField] private CookingPoint[] _cookingPoints;
        [SerializeField] private GiveOrderPoint _giveOrderPoint;
        [SerializeField] private OrderTimer _orderTimer;

        public override void InstallBindings()
        {
            BindSerializeField();
            BindInterfaces();
        }

        private void BindSerializeField()
        {
            SortCookingPoints();

            Container.Bind<CookingsObjectConfig>().FromInstance(_cookingsObject).AsSingle();
            Container.Bind<ActivationPointConfig>().FromInstance(_giveOrderConfig).AsSingle();
            Container.Bind<CookingPoint[]>().FromInstance(_cookingPoints).AsSingle();
            Container.Bind<GiveOrderPoint>().FromInstance(_giveOrderPoint).AsSingle();
            Container.Bind<OrderTimer>().FromInstance(_orderTimer).AsSingle();
        }

        private void BindInterfaces()
        {
            Container.BindInterfacesAndSelfTo<CreatingOrder>().AsSingle();

            Container.BindInterfacesAndSelfTo<RemovingFromList>().AsSingle();
            Container.BindInterfacesAndSelfTo<ActivateCollider>().AsSingle();
            Container.BindInterfacesAndSelfTo<RewardCompletingOrder>().AsSingle();
            Container.BindInterfacesAndSelfTo<CalculationTimeOrder>().AsSingle();
            Container.BindInterfacesAndSelfTo<OrderController>().AsSingle();

            Container.BindInterfacesAndSelfTo<ColliderHandlerGiveOrder>().AsSingle();
            Container.BindInterfacesAndSelfTo<CoinHandler>().AsSingle();
        }

        private void SortCookingPoints()
        {
            ObjectForCoockingConfig[] orderConfigs = _cookingsObject.GetObjects();
            int countPoints = orderConfigs.Length;

            CookingPoint[] sortCookingPoints = new CookingPoint[countPoints];

            for (int i = 0; i < countPoints; i++)
                for (int k = 0; k < countPoints; k++)
                {
                    int intCoockingObj = (int)orderConfigs[i].CookingObject;

                    if (intCoockingObj == _cookingPoints[k].GetIntCookingObj)
                    {
                        sortCookingPoints[i] = _cookingPoints[k];
                        break;
                    }
                }

            _cookingPoints = sortCookingPoints;
        }
    }
}
