using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace YagaClub
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private GameObject ProgressBarCanvas;
        [SerializeField] private Image _progressBar;
        private InteractionWithActivationPoint _interactionWithAP;
        private OpeningPhone _openingPhone;
        private bool _isCanvasActiv = false;

        [Inject]
        private void Constructor(InteractionWithActivationPoint interactionWithAP,
                                 OpeningPhone openingPhone)
        {
            _interactionWithAP = interactionWithAP;
            _openingPhone = openingPhone;
        }

        private void Awake() => SetActiveCanvas(false);

        private void OnPassValue(bool value)
        {
            if (value != _isCanvasActiv)
                SetActiveCanvas(value);

            if (value)
                FillBar();
        }

        private void FillBar()
        {
            ActivityPoint activityPoint = _interactionWithAP.GetActivityPoint;

            if (activityPoint != null)
            {
                UpdateTimer timer = activityPoint.GetTimer;
                float remainigtime = timer.GetRemainigTime;

                if (remainigtime < timer.GetTimeActivations)
                    _progressBar.fillAmount = timer.GetRemainigTime / timer.GetTimeActivations;
                else if (_isCanvasActiv)
                    SetActiveCanvas(false);
            }
        }

        private void SetActiveCanvas(bool value)
        {
            ProgressBarCanvas.SetActive(value);
            _isCanvasActiv = value;
        }

        private void OnDisableCanvasWithOpenPhone(bool value)
        {
            if (value)
                OnPassValue(false);
        }

        private void OnEnable()
        {
            _interactionWithAP.Pressed += OnPassValue;
            _openingPhone.PressedPhone += OnDisableCanvasWithOpenPhone;
        }

        private void OnDisable()
        {
            _interactionWithAP.Pressed -= OnPassValue;
            _openingPhone.PressedPhone -= OnDisableCanvasWithOpenPhone;
        }
    }
}
