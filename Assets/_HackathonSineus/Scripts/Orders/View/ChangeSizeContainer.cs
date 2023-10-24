using UnityEngine;
using UnityEngine.UI;

namespace YagaClub
{
    public class ChangeSizeContainer
    {
        private readonly RectTransform _container;
        private readonly int _numberOfVisibleChild = 4;
        private float _scaleLine;

        public ChangeSizeContainer(RectTransform container)
        {
            _container = container;
            SetScaleStr(container.GetComponent<GridLayoutGroup>().cellSize.y);
        }

        public void SetScaleStr(float lineHeight)
            => _scaleLine = lineHeight;

        public void ChangeSize(int numberLines)
        {
            if (numberLines >= _numberOfVisibleChild)
            {
                _container.sizeDelta = new Vector2(_container.sizeDelta.x,
                    _scaleLine * (numberLines));

                _container.position = new Vector2(_container.position.x,
                     -(_scaleLine * 2) * (numberLines));
            }
        }
    }
}
