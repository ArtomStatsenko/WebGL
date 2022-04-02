using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Create QuestConfig", fileName = "QuestConfig", order = 0)]
public sealed class QuestConfig : ScriptableObject
{
    [SerializeField] private int _id;
    [SerializeField] private QuestType _type;

    public int Id => _id;
    public QuestType Type => _type;
}
