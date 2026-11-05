public class AutoPaddle : Paddle
{
    public Ball ball;
    public AutoPaddleFSM StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new AutoPaddleFSM(this);
        StateMachine.Initialize(StateMachine.Active);
    }

    void Update()
    {
        StateMachine.Update();
    }

    void FixedUpdate()
    {
        StateMachine.FixedUpdate();
    }
}
