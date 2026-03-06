using System;
using Game.GameSquare;
using R3;
using Services;
using UnityEngine;

namespace Game.Tower
{
    public class TowerPresenter : IDisposable
    {
        private readonly TowerModel model;
        private readonly TowerView view;
        private readonly DragService dragService;
        private Camera mainCamera;
        private DisposableBag bag = new();

        public TowerPresenter(TowerModel towerModel, TowerView towerView, DragService dragService, Camera camera)
        {
            model = towerModel;
            view = towerView;
            this.dragService = dragService;
            mainCamera = camera;
            
            model.Squares.OnAdd.Subscribe(squareData =>
            {
                var newSquareView = view.SpawnSquare(squareData.Color, squareData.OffsetX);
                var currentIndex = model.Squares.Count - 1;
                
                OnSquareCreated(newSquareView, currentIndex);
            }).AddTo(ref bag);

            model.Squares.OnRemove.Subscribe(index =>
            {
                view.RemoveAtAndShift(index);
                // messageService.Send("hole");
            }).AddTo(ref bag);
        }

        public bool IsHit(Vector2 screenPoint)
        {
            // If Tower is empty - can place cube everywhere
            if (model.Squares.Count == 0)
                return screenPoint.x > Screen.width * .5f;

            var topSquareRect = view.GetTopSquareRect();
            
            if (topSquareRect == null) return false;
            
            return RectTransformUtility.RectangleContainsScreenPoint(topSquareRect, screenPoint, mainCamera);
        }
        
        public bool IsScreenFull()
        {
            var maxAllowedHeight = view.GetViewRectSize().y;
            var squareHeight = view.GetSquareHeight();
            
            var currentTowerHeight = model.Squares.Count * squareHeight;
            
            return currentTowerHeight + squareHeight > maxAllowedHeight;
        }

        public void OnSquareCreated(SquareView squareView, int index)
        {
            squareView.OnBeginDragStream.Subscribe(ed =>
            {
                var currentIndex = view.GetSquareIndex(squareView);
                
                dragService.OnBeginDrag.OnNext(new DragEventData
                {
                    Color = squareView.Image.color,
                    EventData = ed,
                    Index = currentIndex
                });
            }).AddTo(ref bag);
            
            squareView.OnDragStream.Subscribe(ed =>
            {
                var currentIndex = view.GetSquareIndex(squareView);
                dragService.OnDrag.OnNext(new DragEventData
                {
                    Color = squareView.Image.color,
                    EventData = ed,
                    Index = currentIndex
                });
            }).AddTo(ref bag);
            
            squareView.OnEndDragStream.Subscribe(ed =>
            {
                var currentIndex = view.GetSquareIndex(squareView);
                dragService.OnEndDrag.OnNext(new DragEventData
                {
                    Color = squareView.Image.color,
                    EventData = ed,
                    Index = currentIndex
                });
            }).AddTo(ref bag);
        }
        
        public void Dispose() => bag.Dispose();
    }
}