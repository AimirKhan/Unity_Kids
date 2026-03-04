using UnityEngine;

namespace Game.Hole
{
    public class HolePresenter
    {
        private readonly HoleView view;

        public HolePresenter(HoleView holeView) => view = holeView;
        
        public bool IsInside(Vector2 screenPoint)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(view.Rect,
                screenPoint, null, out var localPoint);
            
            var a = view.Rect.rect.width / 2f;
            var b = view.Rect.rect.height / 2f;

            return (Mathf.Pow(localPoint.x, 2) / Mathf.Pow(a, 2)) +
                (Mathf.Pow(localPoint.y, 2) / Mathf.Pow(b, 2)) <= 1;
        }

        public void OnSwallow() => view.PlaySwallowAnim();
    }
}