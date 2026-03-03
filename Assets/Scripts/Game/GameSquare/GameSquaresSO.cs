using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameSquare
{
    [CreateAssetMenu(fileName = "GameSquare_", menuName = "ScriptableObject/GameSquare", order = 51)]
    public class GameSquaresSO : ScriptableObject
    {
        [SerializeField] List<GameSquare> squares = new();
        
        public List<GameSquare> Squares => squares;
    }

    [Serializable]
    public class GameSquare
    {
        public int Id;
        public Color Color;
    }
}
