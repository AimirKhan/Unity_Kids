using Reflex.Attributes;
using UnityEngine;

namespace Game.Hole
{
    public class HolePresenter
    {
        [Inject]
        private Camera mainCamera;
        private readonly HoleView view;

        public HolePresenter(HoleView holeView) => view = holeView;
        
        public bool IsInside(Vector2 screenPoint)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(view.Rect,
                screenPoint, mainCamera, out var localPoint);
            
            var a = view.Rect.rect.width / 2f;
            var b = view.Rect.rect.height / 2f;

            var isInside = (Mathf.Pow(localPoint.x, 2) / Mathf.Pow(a, 2)) +
                (Mathf.Pow(localPoint.y, 2) / Mathf.Pow(b, 2)) <= 1;
            
            if (isInside) Debug.Log("The square fell into the hole");
            return isInside;
        }

        public Vector2 GetCenterLocal(RectTransform targetParent)
        {
            var worldCenter = view.Rect.position;
            
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                targetParent,
                worldCenter,
                mainCamera,
                out var localPoint);
            
            return localPoint;
        }
        
        public void OnSwallow() => view.PlaySwallowAnim();
    }
}