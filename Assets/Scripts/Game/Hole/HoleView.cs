using DG.Tweening;
using UnityEngine;

namespace Game.Hole
{
    public class HoleView : MonoBehaviour
    {
        [SerializeField] private RectTransform holeRT;
        
        public RectTransform Rect =>  holeRT;

        public void PlaySwallowAnim()
        {
            transform.DOPunchScale(Vector3.one * .1f, .2f);
        }
    }
}