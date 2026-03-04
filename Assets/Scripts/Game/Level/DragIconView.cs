using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Level
{
    public class DragIconView : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private RectTransform rectTransform;

        public void Activate(Color color, Vector2 position)
        {
            image.color = color;
            gameObject.SetActive(true);
            MoveTo(position);
            transform.localScale = Vector3.zero;
            transform.DOScale(1.2f, .15f);
            Debug.Log("DragIconView: Activated");
        }
        
        public void MoveTo(Vector2 screenPosition)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)rectTransform.parent,
                screenPosition,
                null, out var localPosition);
            rectTransform.anchoredPosition = localPosition;
        }

        public void Deactivate(bool isSuccess)
        {
            if (isSuccess)
                gameObject.SetActive(false);
            else
                transform.DOScale(0, .2f).OnComplete(() => gameObject.SetActive(false));
        }
    }
}