using System;
using Cysharp.Threading.Tasks;
using Game.Hole;
using Game.Tower;
using R3;
using Reflex.Attributes;
using Services;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace Game.Level
{
    public class DragIconPresenter : IDisposable
    {
        [Inject]
        private Camera mainCamera;
        private readonly DragService dragService;
        private readonly DragIconView view;
        private readonly HoleView holeView;
        private readonly HolePresenter holePresenter;
        private readonly TowerPresenter towerPresenter;
        private readonly TowerModel towerModel;
        private readonly TowerView towerView;
        private DisposableBag bag = new();

        public DragIconPresenter(DragIconView dragIconView,
            DragService dragService,
            HoleView holeView,
            HolePresenter holePresenter,
            TowerModel towerModel,
            TowerView towerView,
            TowerPresenter towerPresenter)
        {
            view = dragIconView;
            this.dragService = dragService;
            this.holeView = holeView;
            this.holePresenter = holePresenter;
            this.towerModel = towerModel;
            this.towerView = towerView;
            this.towerPresenter = towerPresenter;

            dragService.OnBeginDrag.Subscribe(x =>
                view.Activate(x.Color, x.EventData.position)).AddTo(ref bag);
            
            dragService.OnDrag.Subscribe(x =>
                view.MoveTo(x.EventData.position)).AddTo(ref bag);
                
            // dragService.OnEndDrag.Subscribe(x =>
            //     HandleDrop(x.Item1, x.index).Forget()).AddTo(ref bag);

            dragService.OnEndDrag.SubscribeAwait(async (x, ct) =>
            {
                await HandleDrop(x);
            }, AwaitOperation.Drop).AddTo(ref bag);
            
            Debug.Log("[DragIconPresenter]: Initialized");
        }

        private async UniTask HandleDrop(DragEventData dragData)
        {
            var color = view.CurrentColor;
            
            // 1. Hole check drop
            if (holePresenter.IsInside(dragData.EventData.position))
            {
                await view.PlayAbsorbAnimation(holeView.Rect);
                view.Deactivate(true);
                
                if (dragData.Index.HasValue)
                {
                    // Square from tower
                    towerModel.RemoveAt(dragData.Index.Value);
                    // messageService.Send("hole")
                }
                return;
            }
            
            if (towerPresenter.IsHit(dragData.EventData.position))
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    towerView.Container, dragData.EventData.position, mainCamera, out var localPoint);

                float targetX;
                if (towerModel.Squares.Count == 0)
                {
                    targetX = localPoint.x;
                }
                else
                {
                    var randomOffset = Random.Range(-0.25f, 0.25f) *
                                       towerView.GetTopSquareRect().rect.width;
                    targetX = towerView.GetTopSquareRect().anchoredPosition.x + randomOffset;
                }
                
                if (towerPresenter.IsScreenFull())
                {
                    // messageService.Send("limit");
                    view.Deactivate(false);
                    return;
                }
                towerModel.TryAddCube(color, targetX);
                view.Deactivate(true);
                // messageService.Send("hit");
            }
            else
            {
                view.Deactivate(false);
                // messageService.Send("miss");
            }
        }

        public void Dispose() => bag.Dispose();
    }
}