using System;
using Game.Hole;
using R3;
using Services;
using UnityEngine.EventSystems;

namespace Game.Level
{
    public class DragIconPresenter : IDisposable
    {
        private readonly DragService dragService;
        private readonly DragIconView dragIconView;
        private readonly HolePresenter holePresenter;
        private DisposableBag bag = new();

        public DragIconPresenter(DragService dragService, DragIconView view, HolePresenter hole)
        {
            this.dragService = dragService;
            dragIconView = view;
            holePresenter = hole;

            dragService.OnBeginDrag.Subscribe(x =>
                dragIconView.Activate(x.color, x.Item2.position)).AddTo(ref bag);
            dragService.OnDrag.Subscribe(ed =>
                dragIconView.MoveTo(ed.position)).AddTo(ref bag);
            dragService.OnEndDrag.Subscribe(HandleDrop).AddTo(ref bag);
        }

        private void HandleDrop(PointerEventData ed)
        {
            if (holePresenter.IsInside(ed.position))
            {
                dragIconView.Deactivate(true);
            }
        }

        public void Dispose() => bag.Dispose();
    }
}