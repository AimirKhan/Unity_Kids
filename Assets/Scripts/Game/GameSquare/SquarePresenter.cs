using System;
using R3;
using Reflex.Attributes;
using Services;
using UnityEngine;

namespace Game.GameSquare
{
    public class SquarePresenter : IDisposable
    {
        private readonly SquareModel model;
        private readonly SquareView view;
        private readonly DragService dragService;
        private readonly DisposableBag bag = new();
        
        public SquarePresenter(SquareModel model, SquareView view, DragService dragService)
        {
            this.model = model;
            this.view = view;
            this.dragService = dragService;
            
            view.Init(model.Color);
            
            view.OnBeginDragStream.Subscribe(ed =>
                dragService.OnBeginDrag.OnNext((view.Image.color, ed))).AddTo(ref bag);
            view.OnDragStream.Subscribe(ed => dragService.OnDrag.OnNext(ed)).AddTo(ref bag);
            view.OnDragStream.Subscribe(ed => dragService.OnEndDrag.OnNext(ed)).AddTo(ref bag);
        }
        
        public void Dispose() => bag.Dispose();
    }
}