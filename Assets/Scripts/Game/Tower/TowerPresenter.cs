using System;
using R3;
using UnityEngine;

namespace Game.Tower
{
    public class TowerPresenter : IDisposable
    {
        private readonly TowerModel model;
        private readonly TowerView view;
        private DisposableBag bag = new();

        public TowerPresenter(TowerModel towerModel, TowerView towerView)
        {
            model = towerModel;
            view = towerView;
            
            model.Squares.OnAdd.Subscribe(squareData =>
            {
                view.SpawnSquare(squareData.Color, squareData.OffsetX);
            }).AddTo(ref bag);

            model.Squares.OnRemove.Subscribe(_ =>
            {
                view.RemoveTopAndShiftDown();
            }).AddTo(ref bag);
        }

        // Check on hit tower
        public bool IsHit(Vector2 screenPoint)
        {
            // If Tower is empty - can place cube everywhere
            if (model.Squares.Count == 0)
                return screenPoint.x > Screen.width / 2;

            // Else - check player hit on top cube
            return true;
        }
        
        public bool IsScreenFull(float squareHeight)
        {
            var towerHeight = model.Squares.Count * squareHeight;
            
            return towerHeight >= Screen.height * .8f;
        }

        public void Dispose() => bag.Dispose();
    }
}