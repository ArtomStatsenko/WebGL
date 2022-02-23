using UnityEngine;

public sealed class GameStarter : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private ParalaxMember[] _backgrounds;

    private ParalaxController _paralaxController;

    private void Start()
    {
        _paralaxController = new ParalaxController(_camera.transform, _backgrounds);
    }

    private void Update()
    {
        _paralaxController.Update();
    }

    private void FixedUpdate()
    {
        
    }

    private void OnDestroy()
    {
        
    }
}
