using System;
using R3;
using Services;

namespace Game.GameSquare
{
    public class SquarePresenter : IDisposable
    {
        private readonly SquareModel model;
        private readonly SquareView view;
        private readonly DragService dragService;
        private readonly DisposableBag bag = new();
        
        public SquarePresenter(SquareModel squareModel, SquareView squareView, DragService draggingService)
        {
            model = squareModel;
            view = squareView;
            dragService = draggingService;
            
            squareView.Init(squareModel.Color);
            
            squareView.OnBeginDragStream.Subscribe(eventData => draggingService.OnBeginDrag
                    .OnNext((squareView.Image.color,eventData, squareModel.Id))).AddTo(ref bag);
            
            squareView.OnDragStream.Subscribe(eventData => draggingService.OnDrag
                .OnNext((eventData, null))).AddTo(ref bag);
            
            squareView.OnEndDragStream.Subscribe(eventData => draggingService.OnEndDrag
                .OnNext((eventData, null))).AddTo(ref bag);
        }
        
        public void Dispose() => bag.Dispose();
    }
}