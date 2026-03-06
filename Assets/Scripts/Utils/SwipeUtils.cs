using UnityEngine;

namespace Utils
{
    public enum SwipeDirection
    {
        None = 0,
        Up = 1,
        Right = 2,
        Down = 3,
        Left = 4
    }
    
    public static class SwipeUtils
    {
        public static SwipeDirection GetSwipeDirection(Vector2 delta, float threshold = 5f)
        {
            if (delta.magnitude < threshold) return SwipeDirection.None;
            
            var angle = Mathf.Atan2(delta.y, delta.x) *  Mathf.Rad2Deg;

            if (angle > 45 && angle <= 135) return SwipeDirection.Up;
            if (angle > -135 && angle <= -45) return SwipeDirection.Down;
            if (angle > -45 && angle <= 45) return SwipeDirection.Right;

            return SwipeDirection.Left;
        }
    }
}