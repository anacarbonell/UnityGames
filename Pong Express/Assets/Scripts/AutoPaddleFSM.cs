public class AutoPaddleFSM
{
    public IState CurrentState { get; private set; }
    public IdleState Idle { get; private set; }
    public ActiveState Active { get; private set; }
    public LaunchState Launch { get; private set; }

    public AutoPaddleFSM(AutoPaddle paddle)
    {
        Idle = new IdleState(paddle);
        Active = new ActiveState(paddle);
        Launch = new LaunchState(paddle);
    }

    public void Initialize(IState state)
    {
        CurrentState = state;
        state.Enter();
    }

    public void TransitionTo(IState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        nextState.Enter();
    }

    public void Update()
    {
        CurrentState.Update();
    }

    public void FixedUpdate()
    {
        CurrentState.FixedUpdate();
    }
}
