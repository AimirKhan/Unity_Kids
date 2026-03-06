using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameSquare;
using R3;
using Reflex.Attributes;
using Reflex.Core;
using Services;
using Object = UnityEngine.Object;

namespace Game.SquaresScroll
{
    public class SquaresScrollPresenter : IDisposable
    {
        [Inject]
        private GameSquaresSO gameSquaresSO;
        private readonly Container container;
        private readonly SquaresScrollModel model;
        private readonly SquaresScrollView view;
        private DisposableBag bag = new();
        
        private readonly List<SquarePresenter> activeSquarePresenters = new();
        
        public SquaresScrollPresenter(Container container, SquaresScrollModel model, SquaresScrollView view)
        {
            this.container = container;
            this.model = model;
            this.view = view;
        }
        
        public async UniTask InitializeElements(CancellationToken token)
        {
            var squaresData = gameSquaresSO.Squares;
            var squaresCount = squaresData.Count;
            var squareViews = await Object
                .InstantiateAsync(view.SquarePrefab, squaresCount, view.ScrollRect.content)
                .ToUniTask(cancellationToken: token);

            for (var i = 0; i < squareViews.Length; i++)
            {
                var squareView = squareViews[i];
                var squareData = squaresData[i];
                
                var squareModel = new SquareModel(squareData);
                
                var squarePresenter = new SquarePresenter(squareModel, squareView,
                    container.Resolve<DragService>(), view.ScrollRect);

                activeSquarePresenters.Add(squarePresenter);
            }
            view.BlockerImage.transform.SetAsLastSibling();
        }
        
        public void Dispose() => bag.Dispose();
    }
}
