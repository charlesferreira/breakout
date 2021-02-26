using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SplitBall", menuName = "Breakout/Power Ups/Split Ball", order = 0)]
public class SplitBall : PowerUp
{
    [Range(2, 32)]
    [SerializeField] private int splitInto = 2;

    [Range(0f, 180f)]
    [SerializeField] private float angleBetweenBalls;

    [Range(-180f, 180f)]
    [SerializeField] private float additionalRotation;

    public override void Apply()
    {
        var newBalls = new List<Ball>();
        GameManager.Instance.Balls.ForEach(ball => newBalls.AddRange(Split(ball)));
        GameManager.Instance.Balls.AddRange(newBalls);
    }

    private List<Ball> Split(Ball ball)
    {
        var startingAngle = FixRotation(ball);
        var newBalls = new List<Ball>();

        for (var i = 1; i < splitInto; i++)
        {
            var newAngle = startingAngle + angleBetweenBalls * i;
            newBalls.Add(GameManager.Instance.CreateBall(ball.transform.position, newAngle));
        }

        return newBalls;
    }

    private float FixRotation(Ball ball)
    {
        var fix = additionalRotation - angleBetweenBalls / 2 * (splitInto - 1);
        ball.Velocity = Quaternion.Euler(0, 0, fix) * ball.Velocity;

        return Vector3.SignedAngle(Vector3.up, ball.Velocity, Vector3.forward);
    }
}