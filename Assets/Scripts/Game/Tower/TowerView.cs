using System.Collections.Generic;
using DG.Tweening;
using Game.GameSquare;
using UnityEngine;

namespace Game.Tower
{
    public class TowerView : MonoBehaviour
    {
        [SerializeField] private RectTransform container;
        [SerializeField] private SquareView squarePrefab;
        
        private readonly List<SquareView> activeSquares = new();

        public void SpawnSquare(Color color, float xOffsetNormalized)
        {
            var square = Instantiate(squarePrefab, container);
            square.Init(color);
            activeSquares.Add(square);
            
            var rect = square.GetRectTransform();
            
            // Read position: every next square is higher than previous
            var squreHeight = rect.rect.height;
            var yPosition = (activeSquares.Count - 1) * squreHeight;
            
            // X offset
            var xPosition = xOffsetNormalized * rect.rect.width;
            
            // Start point for anim "arrival"
            rect.anchoredPosition = new Vector2(xPosition, yPosition + 200f);
            rect.localScale = Vector3.zero;
            
            // Setup anim
            var sequence = DOTween.Sequence();
            sequence.Append(rect.DOAnchorPos(new Vector2(xPosition, yPosition),
                .3f).SetEase(Ease.OutBounce));
            sequence.Join(rect.DOScale(1f, .2f));
        }

        public void RemoveTopAndShiftDown()
        {
            if (activeSquares.Count == 0) return;
            
            var lastIndex = activeSquares.Count - 1;
            var topSquare = activeSquares[lastIndex];
            activeSquares.RemoveAt(lastIndex);
            
            // Flying animation into a hole
            topSquare.transform.DOScale(0, .2f)
                .OnComplete(() => Destroy(topSquare.gameObject));
        }
    }
}