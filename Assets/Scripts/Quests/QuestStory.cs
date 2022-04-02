using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class QuestStory : IQuestStory
{
    private readonly List<IQuest> _questCollection;

    public bool IsDone => _questCollection.All(value => value.IsCompleted);

    public QuestStory(List<IQuest> questCollection)
    {
        _questCollection = questCollection;
        Subscribe();
        ResetQuest(0);
    }

    public void Dispose()
    {
        Unsubscribe();

        foreach (var quest in _questCollection)
        {
            quest.Dispose();
        }
    }

    private void Subscribe()
    {
        foreach (var quest in _questCollection)
        {
            quest.Completed += OnQuestCompleted;
        }
    }

    private void Unsubscribe()
    {
        foreach (var quest in _questCollection)
        {
            quest.Completed -= OnQuestCompleted;
        }
    }

    private void OnQuestCompleted(object sender, IQuest quest)
    {
        int index = _questCollection.IndexOf(quest);

        if (IsDone)
        {
            Debug.Log("Story done!");
        }
        else
        {
            ResetQuest(++index);
        }
    }

    private void ResetQuest(int index)
    {
        if (index < 0 || index >= _questCollection.Count)
        {
            return;
        }

        var nextQuest = _questCollection[index];

        if (nextQuest.IsCompleted)
        {
            OnQuestCompleted(this, nextQuest);
        }
        else
        {
            _questCollection[index].Reset();
        }
    }
}

