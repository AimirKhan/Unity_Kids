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
                .OnNext(new DragEventData
                {
                    Color = squareView.Image.color,
                    EventData = eventData,
                    Index = squareModel.Id
                })).AddTo(ref bag);
            
            squareView.OnDragStream.Subscribe(eventData => draggingService.OnDrag
                .OnNext(new DragEventData
                {
                    Color = squareView.Image.color,
                    EventData = eventData
                })).AddTo(ref bag);
            
            squareView.OnEndDragStream.Subscribe(eventData => draggingService.OnEndDrag
                .OnNext(new DragEventData
                {
                    Color = squareView.Image.color,
                    EventData = eventData
                })).AddTo(ref bag);
        }
        
        public void Dispose() => bag.Dispose();
    }
}