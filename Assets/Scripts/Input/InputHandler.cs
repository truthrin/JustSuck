using Hmxs.Toolkit.Base.Singleton;

public class InputHandler : SingletonMono<InputHandler>
{
    public bool IsSucking { get; private set; }

    private InputControls _inputControls;

    protected override void Awake()
    {
        base.Awake();
        _inputControls = new InputControls();
        _inputControls.Enable();
    }

    private void OnDestroy() => _inputControls.Disable();

    private void Update()
    {
        IsSucking = _inputControls.Gameplay.Suck.inProgress;
    }
}
