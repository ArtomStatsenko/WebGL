using UnityEngine;

public class SwitchQuestModel : IQuestModel
{
    public bool TryComplete(GameObject activator)
    {
        return activator.TryGetComponent<PlayerView>(out _);
    }
}
