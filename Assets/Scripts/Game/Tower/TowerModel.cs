using System.R3Ext;
using Game.GameSquare;
using UnityEngine;

namespace Game.Tower
{
    public class TowerModel
    {
        public readonly ReactiveList<SquareData> Squares = new();

        public void TryAddCube(Color color, float xOffset)
        {
            var newData = new SquareData { Color = color, OffsetX = xOffset };
            Squares.Add(newData);
            // Call SaveSystem.Save()
        }

        public void RemoveTop()
        {
            if (Squares.Count > 0)
            {
                Squares.RemoveAt(Squares.Count - 1);
            }
        }

        public void RemoveAt(int index)
        {
            if (index >= 0 && index < Squares.Count)
            {
                Squares.RemoveAt(index);
                Debug.Log($"[TowerModel] Removing square index {index} squares list size {Squares.Count}");
            }
        }
    }
}