using System.Collections.Generic;
using System.R3Ext;
using Game.GameSquare;
using Reflex.Attributes;
using Services.Save;
using Services.Save.Game;
using UnityEngine;

namespace Game.Tower
{
    public class TowerModel
    {
        private const string TOWER_KEY = "UserTowerProgress";
        
        [Inject]
        private readonly SaveService saveService;
        
        public readonly ReactiveList<SquareData> Squares = new();

        private bool isDirty;
        
        public void TryAddCube(Color color, float xOffset)
        {
            Squares.Add(new SquareData { Color = color, OffsetX = xOffset });
            
            isDirty = true;
            //GetSaveSnapshot();
        }

        public void RemoveAt(int index)
        {
            Squares.RemoveAt(index);
            
            isDirty = true;
            //GetSaveSnapshot();
        }
        
        private List<SquareSaveData> GetSaveSnapshot()
        {
            var list = new List<SquareSaveData>();
            for (int i = 0; i < Squares.Count; i++)
            {
                var s = Squares[i];
                list.Add(new SquareSaveData()
                {
                    Id = s.Id,
                    Color = new ColorDto(s.Color),
                    OffsetX = s.OffsetX
                });
            }
            
            return list;
        }

        public void Save()
        {
            saveService.Save(TOWER_KEY, GetSaveSnapshot());
        }
        
        public void ForceSave()
        {
            if (!isDirty) return;

            var snapshot = GetSaveSnapshot();
            saveService.Save(TOWER_KEY, snapshot);
            isDirty = false;
            Debug.Log("[TowerModel] Progress force saved");
        }
        
        public void LoadData()
        {
            var loadedList = saveService.Load<List<SquareSaveData>>(TOWER_KEY);

            if (loadedList != null)
            {
                for (int i = 0; i < loadedList.Count; i++)
                {
                    TryAddCube(loadedList[i].Color.ToColor(), loadedList[i].OffsetX);
                }
            }
        }
    }
}