using UnityEngine;
using UnityEngine.UI;

namespace Game.SquaresScroll
{
    public class SquaresScrollView : MonoBehaviour
    {
        [SerializeField] private ScrollRect scrollRect;

        private void Awake()
        {
            if (!scrollRect)
            {
                scrollRect = GetComponent<ScrollRect>();
            }
        }

        void Start()
        {
            
        }
    }
}
