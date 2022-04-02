using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Create QuestItemsConfig", fileName = "QuestItemsConfig", order = 0)]
public sealed class QuestItemsConfig : ScriptableObject
{
    [SerializeField] private int _questId;
    [SerializeField] private List<int> _itemIdCollection;

    public int QuestId => _questId;
    public List<int> ItemIdCollection => _itemIdCollection;
}
