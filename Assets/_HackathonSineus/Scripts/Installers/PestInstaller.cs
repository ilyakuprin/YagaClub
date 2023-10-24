using UnityEngine;
using Zenject;

namespace YagaClub
{
    public class PestInstaller : MonoInstaller
    {
        [SerializeField] private PestsControlPointsConfig _pestsControlPointsConfig;
        [SerializeField] private DishQualityDegradationHandler[] _dishQualityDegradation;
        private PestControlPoint[] _pestControlPoint;

        public override void InstallBindings()
        {
            BindSerializeField();
        }

        private void BindSerializeField()
        {
            SortDishQualityDegradation();
            FillArrayPestControlPoint();

            Container.Bind<PestsControlPointsConfig>().FromInstance(_pestsControlPointsConfig).AsSingle();
            Container.Bind<DishQualityDegradationHandler[]>().FromInstance(_dishQualityDegradation).AsSingle();
            Container.Bind<PestControlPoint[]>().FromInstance(_pestControlPoint).AsSingle();
        }

        private void SortDishQualityDegradation()
        {
            PestControlPointConfig[] orderConfigs = _pestsControlPointsConfig.GetObjects();
            int countPoints = orderConfigs.Length;

            DishQualityDegradationHandler[] sortPestControlPoint = new DishQualityDegradationHandler[countPoints];

            for (int i = 0; i < countPoints; i++)
                for (int k = 0; k < countPoints; k++)
                {
                    int intCoockingObj = (int)orderConfigs[i].CookingObject;

                    if (intCoockingObj == _dishQualityDegradation[k].GetPestControlPoint.GetIntCookingObj)
                    {
                        sortPestControlPoint[i] = _dishQualityDegradation[k];
                        break;
                    }
                }

            _dishQualityDegradation = sortPestControlPoint;
        }

        private void FillArrayPestControlPoint()
        {
            _pestControlPoint = new PestControlPoint[_dishQualityDegradation.Length];

            for (int i = 0; i < _pestControlPoint.Length; i++)
                _pestControlPoint[i] = _dishQualityDegradation[i].GetPestControlPoint;
        }
    }
}
