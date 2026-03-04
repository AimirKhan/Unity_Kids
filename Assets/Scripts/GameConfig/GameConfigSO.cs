using Game.GameSquare;
using UnityEngine;

namespace GameConfig
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObject/Create Game Config", order = 66)]
    public class GameConfigSO : ScriptableObject
    {
        public GameSquaresSO GameSquaresSO;
    }
}
