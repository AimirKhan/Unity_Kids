using System;
using DG.Tweening;
using R3;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.GameSquare
{
    public class SquareView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public readonly Subject<PointerEventData> OnBeginDragStream = new();
        public readonly Subject<PointerEventData> OnDragStream = new();
        public readonly Subject<PointerEventData> OnEndDragStream = new();
        
        [SerializeField] private Image image;
        
        public Image Image => image;
        public RectTransform GetRectTransform() => image.rectTransform;
        
        private ScrollRect scrollRect;
        private bool isDragging;
        
        public void Init(Color color, ScrollRect scroll = null)
        {
            image.color = color;
            scrollRect = scroll;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (scrollRect == null)
            {
                StartSquareDrag(eventData);
                return;
            }
            
            var deltaX = Mathf.Abs(eventData.delta.x);
            var deltaY = Mathf.Abs(eventData.delta.y);

            if (deltaY > deltaX)
            {
                StartSquareDrag(eventData);
            }
            else
            {
                isDragging = false;
                scrollRect.OnBeginDrag(eventData);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (isDragging)
                OnDragStream.OnNext(eventData);
            else if (scrollRect != null)
                scrollRect.OnDrag(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (isDragging)
            {
                if (scrollRect != null)
                    scrollRect.enabled = true;
                OnEndDragStream.OnNext(eventData);
                isDragging = false;
            }
            else if (scrollRect != null)
            {
                scrollRect.OnEndDrag(eventData);
            }
        }

        private void StartSquareDrag(PointerEventData eventData)
        {
            isDragging = true;
            if (scrollRect != null) scrollRect.enabled = false;
            OnBeginDragStream.OnNext(eventData);
        }
        
        public void PlayDisappearAnim(Action onComplete)
        {
            transform.DOScale(Vector3.zero, 0.2f)
                .SetEase(Ease.OutBack)
                .OnComplete(() => onComplete?.Invoke());
        }
        
        private void Dispose()
        {
            OnBeginDragStream.OnCompleted();
            OnDragStream.OnCompleted();
            OnEndDragStream.OnCompleted();
        }

        private void OnDestroy() => Dispose();
    }
}
