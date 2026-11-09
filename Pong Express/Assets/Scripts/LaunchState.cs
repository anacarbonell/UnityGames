using UnityEngine;

public class LaunchState : IState
{
    private AutoPaddle paddle;
    private float timer = 0f;
    private float timeToLaunch = 2f;

    public LaunchState(AutoPaddle paddle)
    {
        this.paddle = paddle;
    }

    public void Enter()
    {
        Debug.Log("CPU: Launch");
    }

    public void Exit() { }

    public void FixedUpdate() { }

    public void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToLaunch)
        {
            timer = 0f;
            paddle.gameManager.LaunchBall();
            paddle.StateMachine.TransitionTo(paddle.StateMachine.Idle);
        }
    }
}
