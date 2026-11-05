using UnityEngine;

public class ActiveState : IState
{
    private AutoPaddle paddle;

    public ActiveState(AutoPaddle paddle)
    {
        this.paddle = paddle;
    }

    public void Enter() { }

    public void Exit() { }

    public void FixedUpdate()
    {
        float difference = paddle.ball.transform.position.y - paddle.transform.position.y;
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
