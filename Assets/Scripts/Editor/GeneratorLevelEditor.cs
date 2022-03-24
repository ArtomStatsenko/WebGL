using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelGeneratorView))]
public sealed class GeneratorLevelEditor : Editor
{
    private LevelGeneratorController _controller;

    private void OnEnable()
    {
        var view = (LevelGeneratorView)target;
        _controller = new LevelGeneratorController(view);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if (GUI.Button(new Rect(10, 0, 70, 25), "Generate"))
        {
            _controller.Clear();
            _controller.Generate();
        }

        if (GUI.Button(new Rect(10, 30, 70, 25), "Clear"))
        {
            _controller.Clear();
        }

        GUILayout.Space(50);

        serializedObject.ApplyModifiedProperties();
    }
}
