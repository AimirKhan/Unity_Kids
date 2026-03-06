using System.Collections.Generic;
using UnityEngine;

namespace Services.Save.Game
{
    [System.Serializable]
    public class TowerSaveData : SaveData
    {
        public List<Color> Colors = new();
        public List<float> Offsets = new();
    }
}