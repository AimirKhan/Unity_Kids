using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Services.MessageService
{
    public class MessageView : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        private Tween fadeTween;

        public void Show(string content)
        {
            text.text = content;
            
            fadeTween?.Kill();
            text.alpha = 1;
            transform.localScale = Vector3.one * .8f;
            transform.DOScale(1f, .3f).SetEase(Ease.OutBack);

            fadeTween = text.DOFade(0, 1f).SetDelay(1.5f);
        }
    }
}