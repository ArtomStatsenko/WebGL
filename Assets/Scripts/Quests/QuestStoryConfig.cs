using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Create QuestStoryConfig", fileName = "QuestStoryConfig", order = 0)]
public sealed class QuestStoryConfig : ScriptableObject
{
    [SerializeField] private QuestConfig[] _quests;
    [SerializeField] private QuestStoryType _type;

    public QuestConfig[] Quests => _quests;
    public QuestStoryType Type => _type;
}
