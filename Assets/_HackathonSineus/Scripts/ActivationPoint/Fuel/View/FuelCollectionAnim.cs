using UnityEngine;
using Zenject;

namespace YagaClub
{
    [RequireComponent(typeof(Animator))]
    public class FuelCollectionAnim : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private GettingFuelPoint _fuelPoint;
        private int _cashParameter;

        [Inject]
        private void Construct(GettingFuelPoint fuelPoint)
            => _fuelPoint = fuelPoint;

        private void Awake()
            => _cashParameter = new StringAnimationParameters().Change;

        private void TakeFuel(bool value)
            => _animator.SetBool(_cashParameter, value);

        private void OnEnable()
            => _fuelPoint.TookFuel += TakeFuel;

        private void OnDisable()
            => _fuelPoint.TookFuel += TakeFuel;

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
        }
    }
}
