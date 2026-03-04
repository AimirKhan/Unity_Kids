using System.Collections.Generic;
using Game.GameSquare;
using Reflex.Attributes;
using UnityEngine;

namespace Game.SquaresScroll
{
    public class SquaresScrollPresenter
    {
        [Inject] GameSquaresSO gameSquaresSO;
        private SquaresScrollModel model;
        private SquaresScrollView view;
        private readonly List<SquarePresenter> squares = new();
        
        public SquaresScrollPresenter(SquaresScrollModel model, SquaresScrollView view)
        {
            this.model = model;
            this.view = view;
        }
        
        public void InitializeElements()
        {
            foreach (var squareData in gameSquaresSO.Squares)
            {
                var squareModel = new SquareModel(squareData);
                var squareView = Object.Instantiate(view.SquarePrefab, view.ScrollRect.content);
                var squarePresenter = new SquarePresenter(squareModel, squareView);
                
                squares.Add(squarePresenter);
            }
        }
    }
}
