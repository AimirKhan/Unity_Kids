using UnityEngine;
using UnityEngine.UI;

namespace Game.GameSquare
{
    public class SquareView : MonoBehaviour
    {
        [SerializeField] private Image image;
        
        public void Init(Color color) => image.color = color;
    }
}
