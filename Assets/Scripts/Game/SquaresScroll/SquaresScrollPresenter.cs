using System.Collections.Generic;
using System.Reflection.Emit;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameSquare;
using Game.Tower;
using Reflex.Attributes;
using Reflex.Core;
using Services;
using UnityEngine;

namespace Game.SquaresScroll
{
    public class SquaresScrollPresenter
    {
        [Inject] GameSquaresSO gameSquaresSO;
        private readonly Container container;
        private readonly SquaresScrollModel model;
        private readonly SquaresScrollView view;
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
                    container.Resolve<DragService>());

                activeSquarePresenters.Add(squarePresenter);
            }
        }
    }
}
