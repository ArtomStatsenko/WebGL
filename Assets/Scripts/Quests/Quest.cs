using System;

public sealed class Quest : IQuest
{
    public event EventHandler<IQuest> Completed;

    private readonly QuestObjectView _view;
    private readonly IQuestModel _model;

    private bool _isActive;

    public bool IsCompleted { get; private set; }

    public Quest(QuestObjectView view, IQuestModel model)
    {
        _view = view;
        _model = model;
    }

    public void Dispose()
    {
        _view.OnLevelObjectContactedEvent -= OnContact;
    }

    public void Reset()
    {
        if (_isActive)
        {
            return;
        }

        _isActive = true;
        IsCompleted = false;
        _view.OnLevelObjectContactedEvent += OnContact;
        _view.ActivateProcess();
    }

    private void OnContact(LevelObjectView contactedView)
    {
        bool completed = _model.TryComplete(contactedView.gameObject);

        if (completed)
        {
            Complete();
        }
    }

    private void Complete()
    {
        if (!_isActive)
        {
            return;
        }

        _isActive = false;
        IsCompleted = true;
        _view.OnLevelObjectContactedEvent -= OnContact;
        _view.CompleteProcess();
        OnCompleted();
    }

    private void OnCompleted()
    {
        Completed?.Invoke(this, this);
    }
}

