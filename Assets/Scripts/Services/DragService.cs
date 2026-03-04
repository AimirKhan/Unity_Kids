using R3;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Services
{
    public class DragService
    {
        public readonly Subject<(Color color, PointerEventData)> OnBeginDrag = new();
        public readonly Subject<PointerEventData> OnDrag = new();
        public readonly Subject<PointerEventData> OnEndDrag = new();
    }
}