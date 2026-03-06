using UnityEngine;

namespace Game.GameSquare
{
    public class SquareModel
    {
        public readonly int Id;
        public readonly Color Color;
        
        public SquareModel(SquareData data)
        {
            Id = data.Id;
            Color = data.Color;
        }
    }
}