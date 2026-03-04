using System.Collections.Generic;
using UnityEngine;

namespace Game.GameSquare
{
    [CreateAssetMenu(fileName = "GameSquares", menuName = "ScriptableObject/GameSquare", order = 66)]
    public class GameSquaresSO : ScriptableObject
    {
        [SerializeField] private List<SquareData> squares = new();
        
        public List<SquareData> Squares => squares;
    }
}
