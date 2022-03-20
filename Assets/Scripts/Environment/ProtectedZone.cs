using System.Collections.Generic;
using UnityEngine;

public sealed class ProtectedZone
{
    private readonly List<IProtector> _protectors;
    private readonly LevelObjectTrigger _view;

    public ProtectedZone(LevelObjectTrigger view, List<IProtector> protectors)
    {
        _view = view;
        _protectors = protectors;
    }

    public void Init()
    {
        _view.OnTriggerEnterEvent += OnContact;
        _view.OnTriggerExitEvent += OnExit;
    }

    public void Dispose()
    {
        _view.OnTriggerEnterEvent -= OnContact;
        _view.OnTriggerExitEvent -= OnExit;
    }

    private void OnContact(object sender, GameObject gameObject)
    {
        foreach (var protector in _protectors)
        {
            protector.StartProtection(gameObject);
        }
    }

    private void OnExit(object sender, GameObject gameObject)
    {
        foreach (var protector in _protectors)
        {
            protector.FinishProtection(gameObject);
        }
    }
}