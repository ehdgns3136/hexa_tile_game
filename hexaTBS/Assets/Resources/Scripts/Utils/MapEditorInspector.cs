using Resources.Scripts.InGame;
using UnityEngine;
using UnityEditor;

namespace Resources.Scripts.Utils
{
    [CustomEditor(typeof(MapEditor))]
    public class MapEditorInspector : Editor
    {
        public override void OnInspectorGUI()
        {
//            serializedObject.Update();
            
            MapEditor mapEditor = (MapEditor) target;
            
            SerializedProperty tps = serializedObject.FindProperty ("colors");
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(tps, true);
            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();

            EditorGUILayout.BeginHorizontal();

            for (int i = 0; i < mapEditor.colors.Length; i++)
            {
                if (mapEditor.selectedColor == i)
                {
                    GUI.color = Color.Lerp(Color.black, mapEditor.colors[i], 0.5f);
                }
                else
                {
                    GUI.color = mapEditor.colors[i];
                }
                if (GUILayout.Button(""))
                {
                    mapEditor.SelectColor(i);
                }
            }
            
            EditorGUILayout.EndHorizontal();

            GUI.color = mapEditor.editorDefaultColor;
            
            HexTile.TileHeight selectedHeight = (HexTile.TileHeight) EditorGUILayout.EnumPopup("Height", mapEditor.selectedHeight);
            if (selectedHeight != mapEditor.selectedHeight)
            {
                mapEditor.SelectHeight(selectedHeight);
            }

            EditorGUILayout.BeginHorizontal();

            GUI.color = mapEditor.add ? mapEditor.editorSelectedColor : mapEditor.editorDefaultColor;
            
            if (GUILayout.Button("Add", GUILayout.Height(30)))
            {
                mapEditor.SelectAdd();
            }
            
            GUI.color = mapEditor.remove ? mapEditor.editorSelectedColor : mapEditor.editorDefaultColor;

            if (GUILayout.Button("Remove", GUILayout.Height(30)))
            {
                mapEditor.SelectRemove();
            }
            
            EditorGUILayout.EndHorizontal();
        }
    }
}