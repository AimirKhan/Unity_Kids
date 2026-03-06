using Game.GameSquare;
using UnityEngine;
using UnityEngine.UI;

namespace Game.SquaresScroll
{
    [RequireComponent(typeof(ScrollRect))]
    public class SquaresScrollView : MonoBehaviour
    {
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private SquareView squarePrefab;
        [SerializeField] private Image blockerImage;
        
        public SquareView SquarePrefab => squarePrefab;
        public ScrollRect ScrollRect => scrollRect;
        public Image BlockerImage => blockerImage;
        
    }
}
