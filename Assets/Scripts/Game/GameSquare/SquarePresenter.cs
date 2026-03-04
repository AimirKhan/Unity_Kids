using R3;

namespace Game.GameSquare
{
    public class SquarePresenter
    {
        private readonly SquareModel model;
        private readonly SquareView view;
        private readonly DisposableBag bag = new();
        
        public SquarePresenter(SquareModel model, SquareView view)
        {
            this.model = model;
            this.view = view;
            
            view.Init(model.Color);
            // Add logic fofr R3
        }
        
        public void Dispose() => bag.Dispose();
    }
}