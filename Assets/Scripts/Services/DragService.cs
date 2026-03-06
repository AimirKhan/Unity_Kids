using R3;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Services
{
    public class DragService
    {
        public readonly Subject<DragEventData> OnBeginDrag = new();
        public readonly Subject<DragEventData> OnDrag = new();
        public readonly Subject<DragEventData> OnEndDrag = new();
    }

    public struct DragEventData
    {
        public Color Color;
        public PointerEventData EventData;
        public int? Index;
    }
}