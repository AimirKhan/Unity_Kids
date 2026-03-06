using Cysharp.Threading.Tasks;
using DG.Tweening;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Level
{
    public class DragIconView : MonoBehaviour
    {
        [Inject] private Camera mainCamera;
        
        [SerializeField] private Image image;
        
        public Color CurrentColor => currentColor;

        private Color currentColor;
        
        public void Activate(Color color, Vector2 position)
        {
            image.color = currentColor = color;
            gameObject.SetActive(true);
            MoveTo(position);
            transform.localScale = Vector3.zero;
            transform.DOScale(1.2f, .15f);
            Debug.Log("[DragIconView]: Activated");
        }
        
        public void MoveTo(Vector2 screenPosition)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)transform.parent,
                screenPosition,
                mainCamera, out var localPosition);
            image.rectTransform.anchoredPosition = localPosition;
        }

        public void Deactivate(bool isSuccess)
        {
            if (isSuccess)
                gameObject.SetActive(false);
            else
                transform.DOScale(0, .2f).OnComplete(() => gameObject.SetActive(false));
            
            Debug.Log("DragIconView: Deactivated");
        }

        public async UniTask PlayAbsorbAnimation(RectTransform holeTarget)
        {
            var startColor = image.color;
            var sequence = DOTween.Sequence();
            sequence.Join(image.rectTransform.DOMove(holeTarget.position, 0.25f).SetEase(Ease.InBack));
            sequence.Join(transform.DOScale(0, 0.25f).SetEase(Ease.InBack));
            sequence.Join(image.DOFade(0, 0.2f));
            
            await sequence.Play().ToUniTask();
    
            gameObject.SetActive(false);
            image.color = startColor;
        }

        public Vector2 GetSquareSize()
        {
            return image.rectTransform.rect.size;
        }
    }
}