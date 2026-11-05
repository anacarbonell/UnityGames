using UnityEngine;

public class IdleState : IState
{
    private AutoPaddle paddle;

    public IdleState(AutoPaddle paddle)
    {
        this.paddle = paddle;
    }

    public void Enter()
    {
        Debug.Log("CPU: Idle");
    }

    public void Exit() { }

    public void FixedUpdate()
    {
        if (paddle.ball.rb.linearVelocityX > 0)
        {
            paddle.StateMachine.TransitionTo(paddle.StateMachine.Active);
            return;
        }

        float difference = paddle.StartPosition.y - paddle.transform.position.y;
        float movement = 0f;

        if (difference > 0.1f)
        {
            movement = 1;
        }
        else if (difference < -0.1f)
        {
            movement = -1;
        }

        paddle.rb.linearVelocityY = movement * paddle.speed;
    }

    public void Update() { }
}
