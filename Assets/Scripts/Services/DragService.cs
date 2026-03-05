using R3;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Services
{
    public class DragService
    {
        public readonly Subject<(Color color, PointerEventData, int index)> OnBeginDrag = new();
        public readonly Subject<(PointerEventData, int? index)> OnDrag = new();
        public readonly Subject<(PointerEventData, int? index)> OnEndDrag = new();
    }
}