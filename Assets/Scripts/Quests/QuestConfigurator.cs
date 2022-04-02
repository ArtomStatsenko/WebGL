using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class QuestConfigurator : MonoBehaviour
{
    [SerializeField] private QuestObjectView _singleQuestView;
    [SerializeField] private QuestStoryConfig[] _questStoryConfigs;
    [SerializeField] private QuestObjectView[] _questObjects;

    private Quest _singleQuest;
    private List<IQuestStory> _questStories;

    private readonly Dictionary<QuestType, Func<IQuestModel>> _questFactories = new Dictionary<QuestType, Func<IQuestModel>>()
    {
        { QuestType.Switch, () => new SwitchQuestModel() }
    };

    private readonly Dictionary<QuestStoryType, Func<List<IQuest>, IQuestStory>> _questStoryFactories = new Dictionary<QuestStoryType, Func<List<IQuest>, IQuestStory>>
    {
        { QuestStoryType.Common, questCollection => new QuestStory(questCollection) },
        { QuestStoryType.Resettable, questCollection => new ResettableQuestStory(questCollection) },
    };

    private void Start()
    {
        _singleQuest = new Quest(_singleQuestView, new SwitchQuestModel());
        _singleQuest.Reset();

        _questStories = new List<IQuestStory>();
        foreach (var questStoryConfig in _questStoryConfigs)
        {
            _questStories.Add(CreateQuestStory(questStoryConfig));
        }

    }

    private void OnDestroy()
    {
        _singleQuest.Dispose();

        foreach (var questStory in _questStories)
        {
            questStory.Dispose();
        }

        _questStories.Clear();
    }

    private IQuestStory CreateQuestStory(QuestStoryConfig config)
    {
        var quests = new List<IQuest>();
        
        foreach (var questConfig in config.Quests)
        {
            var quest = CreateQuest(questConfig);

            if (quest == null)
            {
                continue;
            }

            quests.Add(quest);
        }

        return _questStoryFactories[config.Type].Invoke(quests);
    }

    private IQuest CreateQuest(QuestConfig config)
    {
        int questId = config.Id;
        QuestObjectView questView = _questObjects.FirstOrDefault(value => value.Id == config.Id);

        if (questView == null)
        {
            return null;
        }

        if (_questFactories.TryGetValue(config.Type, out var factory))
        {
            IQuestModel questModel = factory.Invoke();
            return new Quest(questView, questModel);
        }

        return null;
    }
}
