using UnityEngine;

public class QuestObjectView : LevelObjectView
{
    [SerializeField] private int _id;
    [SerializeField] private Color _completedColor;

    private Color _defaultColor;

    public int Id => _id;

    private void Awake()
    {
        _defaultColor = Renderer.color;
    }

    public void CompleteProcess()
    {
        Renderer.color = _completedColor;
    }

    public void ActivateProcess()
    {
        Renderer.color = _defaultColor;
    }
}

