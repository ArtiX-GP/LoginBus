using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(Maze))]
    public class MazeEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Maze myScript = (Maze)target;
            if (GUILayout.Button("Generate field"))
            {
                myScript.GenerateField();
            }
        }

    }
}