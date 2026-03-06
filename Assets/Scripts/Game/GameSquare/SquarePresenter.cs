using System;
using R3;
using Services;
using UnityEngine.UI;

namespace Game.GameSquare
{
    public class SquarePresenter : IDisposable
    {
        private readonly SquareModel model;
        private readonly SquareView view;
        private readonly DragService dragService;
        private readonly ScrollRect scrollRect;
        private DisposableBag bag = new();
        
        public SquarePresenter(SquareModel squareModel,
            SquareView squareView,
            DragService draggingService,
            ScrollRect scrollRect)
        {
            model = squareModel;
            view = squareView;
            dragService = draggingService;
            this.scrollRect = scrollRect;
            
            squareView.Init(squareModel.Color, scrollRect);
            
            squareView.OnBeginDragStream.Subscribe(eventData =>
            {
                if (squareModel.Id != -1)
                    squareView.gameObject.SetActive(true);
                
                draggingService.OnBeginDrag
                    .OnNext(new DragEventData
                    {
                        Color = squareView.Image.color,
                        EventData = eventData,
                        Index = squareModel.Id
                    });
            }).AddTo(ref bag);
            
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