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
        
        public RectTransform Container => container;
        
        private readonly List<SquareView> activeSquares = new();

        public SquareView SpawnSquare(Color color, float xOffsetNormalized)
        {
            var square = Instantiate(squarePrefab, container);
            square.Init(color);
            activeSquares.Add(square);
            
            var squareRT = square.GetRectTransform();
            
            var squareRect = squareRT.rect;
            
            // offset
            var xPosition = xOffsetNormalized;
            var yPosition = (activeSquares.Count - 1) * squareRect.height;
            
            // Start point for anim "arrival"
            var targetPosition = new Vector2(xPosition,
                yPosition + (container.rect.position.y / 2) - squareRect.y);
            Debug.Log("Square target position: " + targetPosition + " source position: " + yPosition);
            Debug.Log("Tower container value: " + container.rect.position);
            squareRT.anchoredPosition = new Vector2(xPosition, yPosition + 500f);
            squareRT.localScale = Vector3.zero;
            
            // Setup anim
            var sequence = DOTween.Sequence();
            sequence.Append(squareRT
                .DOAnchorPos(targetPosition, .3f)
                .SetEase(Ease.OutBounce));
            sequence.Join(squareRT.DOScale(1f, .2f));
            
            return square;
        }

        public RectTransform GetTopSquareRect()
        {
            if (activeSquares.Count == 0) return null;
            return activeSquares[activeSquares.Count - 1].GetRectTransform();
        }

        public Vector2 GetViewRectSize() => container.rect.size;
        
        public float GetSquareHeight()
        {
            return squarePrefab.GetRectTransform().rect.height;
        }
        
        public void RemoveAtAndShift(int index)
        {
            var squareToRemove = activeSquares[index];
            activeSquares.RemoveAt(index);
            
            Destroy(squareToRemove.gameObject);

            for (int i = 0; i < activeSquares.Count; i++)
            {
                var rect = activeSquares[i].GetRectTransform();
                var rectOffset = (container.rect.position.y / 2) + GetSquareHeight() / 2;
                var newY = i * GetSquareHeight() + rectOffset;
                
                rect.DOAnchorPosY(newY, .5f).SetEase(Ease.OutBounce);
            }
        }
    }
}