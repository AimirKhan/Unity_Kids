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
        
        public void Init(Color color) => image.color = color;
        
        public void OnBeginDrag(PointerEventData eventData) => OnBeginDragStream.OnNext(eventData);
        public void OnDrag(PointerEventData eventData) => OnDragStream.OnNext(eventData);
        public void OnEndDrag(PointerEventData eventData) => OnEndDragStream.OnNext(eventData);

        public void PlayDisappearAnim(Action onComplete)
        {
            transform.DOScale(Vector3.zero, 0.3f)
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
