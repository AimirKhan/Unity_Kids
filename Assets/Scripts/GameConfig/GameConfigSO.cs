using System;
using Game.GameSquare;
using Localization;
using UnityEngine;

namespace GameConfig
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObject/Create Game Config", order = 66)]
    public class GameConfigSO : ScriptableObject
    {
        public GameSquaresSO GameSquaresSO;
        public LocalizationConfig LocalizationConfig;
    }
}
