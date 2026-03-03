using Game.GameSquare;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(GameSquaresSO))]
    public class DataConfigEditor : UnityEditor.Editor
    {
        private int selectedIndex = -1;

        private int currentIntId;
        private Color currentColor = Color.white;
        
        public override void OnInspectorGUI()
        {
            var gameSquaresSo = (GameSquaresSO)target;
            
            // Create section
            EditorGUILayout.BeginVertical("box"); // Draw border around editor
            
            EditorGUI.BeginChangeCheck();
            currentIntId = EditorGUILayout.IntField("Next Id", currentIntId);
            currentColor = EditorGUILayout.ColorField("Next Color", currentColor);
            
            if (selectedIndex >= 0 && selectedIndex < gameSquaresSo.Squares.Count)
            {
                var tempItem = gameSquaresSo.Squares[selectedIndex];
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(gameSquaresSo, "Edit Item");
                    tempItem.Id = currentIntId;
                    tempItem.Color = currentColor;
                    EditorUtility.SetDirty(gameSquaresSo);
                }
            }
            EditorGUILayout.EndVertical();
            
            // 1. Create button
            if (GUILayout.Button("Create New Item", GUILayout.Height(30)))
            {
                Undo.RecordObject(gameSquaresSo, "Add New Item");
                var newIntId = gameSquaresSo.Squares.Count - 1;
                var newItem = new GameSquare()
                {
                    Id = newIntId + 1, // ID Generation
                    Color = new Color(Random.value, Random.value, Random.value, 1)
                };
                currentIntId = newItem.Id;
                currentColor = newItem.Color;
                selectedIndex = currentIntId;
                gameSquaresSo.Squares.Add(newItem);
                EditorUtility.SetDirty(gameSquaresSo);
            }
            
            // 2. Delete button
            using (new EditorGUI.DisabledGroupScope(selectedIndex < 0))
            {
                if (GUILayout.Button("Delete Item"))
                {
                    Undo.RecordObject(gameSquaresSo, "Delete Item");
                    gameSquaresSo.Squares.RemoveAt(selectedIndex);
                    selectedIndex -= 1;
                    if (selectedIndex < 1 && gameSquaresSo.Squares.Count > 0)
                    {
                        selectedIndex = 0;
                    }
                    EditorUtility.SetDirty(gameSquaresSo);
                }
                
                if (GUILayout.Button("Deselect Item"))
                {
                    Undo.RecordObject(gameSquaresSo, "Deselect Item");
                    currentIntId = gameSquaresSo.Squares.Count;
                    selectedIndex = -1;
                    EditorUtility.SetDirty(gameSquaresSo);
                }
            }
            
            GUILayout.Space(10);
            EditorGUILayout.LabelField("List squares elements:", EditorStyles.boldLabel);
            
            // 3. Buttons (select elements)
            for (int i = 0; i < gameSquaresSo.Squares.Count; i++)
            {
                var item = gameSquaresSo.Squares[i];
                var totalWidth = EditorGUIUtility.currentViewWidth;
                
                EditorGUILayout.BeginHorizontal();
                
                GUI.backgroundColor = (selectedIndex == i) ? Color.cyan : Color.white;

                EditorGUI.BeginChangeCheck();
                if (GUILayout.Button($"ID: {gameSquaresSo.Squares[i].Id}"))
                {
                    GUI.FocusControl(null); // Unfocus input fields
                }
                
                var newColor = EditorGUILayout.ColorField(item.Color, GUILayout.Width(totalWidth * .2f));
                if (EditorGUI.EndChangeCheck())
                {
                    selectedIndex = i;
                    Undo.RecordObject(gameSquaresSo, "Change color in list");
                    item.Color = newColor;
                    currentIntId = item.Id;
                    currentColor = item.Color;
                    EditorUtility.SetDirty(gameSquaresSo);
                }
                
                EditorGUILayout.EndHorizontal();
            }
            GUI.backgroundColor = Color.white;
        }
    }
}
