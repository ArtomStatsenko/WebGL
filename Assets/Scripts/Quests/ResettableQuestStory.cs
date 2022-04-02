using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public sealed class ResettableQuestStory : IQuestStory
{
    private readonly List<IQuest> _questCollection;
    private int _currentIndex;

    public bool IsDone => _questCollection.All(value => value.IsCompleted);

    public ResettableQuestStory(List<IQuest> questCollection)
    {
        _questCollection = questCollection;
        Subscribe();
        ResetQuests();
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

        if (_currentIndex == index)
        {
            _currentIndex++;

            if (IsDone)
            {
                Debug.Log("Story done!");
            }
        }
        else
        {
            ResetQuests();
        }
    }

    private void ResetQuests()
    {
        _currentIndex = 0;

        foreach (var quest in _questCollection)
        {
            quest.Reset();
        }
    }
}
