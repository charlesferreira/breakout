using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] Transform ballSpawnPoint;
    [SerializeField] Transform padSpawnPoint;

    [SerializeField] Ball ballPrefab;
    [SerializeField] Pad padPrefab;

    [SerializeField] TextMeshProUGUI text;

    private List<Ball> balls;

    private int _score;
    public int Score
    {
        get { return _score; }
        set
        {
            _score = value;
            text.text = value.ToString();
        }
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        Init();
    }

    private void Init()
    {
        var ball = CreateBall(ballSpawnPoint.position);
        balls = new List<Ball>() { ball };

        Instantiate(padPrefab, padSpawnPoint);
    }

    private Ball CreateBall(Vector3 position, float angle = 0)
    {
        return Instantiate(ballPrefab, position, Quaternion.AngleAxis(angle, Vector3.forward));
    }

    public void GetPowerUp(PowerUpType type)
    {
        switch (type)
        {
            case PowerUpType.SplitBall:
                SplitBall();
                break;

            case PowerUpType.MultiBall:
                MultiBall();
                break;
        }
    }

    private void SplitBall()
    {
        var newBalls = new List<Ball>();
        balls.ForEach(ball =>
        {
            var angle = Vector3.SignedAngle(Vector3.up, ball.Velocity, Vector3.forward) + 22.5f;
            newBalls.Add(CreateBall(ball.transform.position, angle));
            ball.Velocity = Quaternion.Euler(0, 0, -22.5f) * ball.Velocity;
        });
        balls.AddRange(newBalls);
    }

    private void MultiBall()
    {
        var newBalls = new List<Ball>();
        balls.ForEach(ball =>
        {
            for (var i = 1; i < 8; i++)
            {
                var angle = Vector3.SignedAngle(Vector3.up, ball.Velocity, Vector3.forward) + 45f * i;
                newBalls.Add(CreateBall(ball.transform.position, angle));
            }
        });
        balls.AddRange(newBalls);
    }

    public void RemoveBall(Ball ball)
    {
        balls.Remove(ball);
        Destroy(ball.gameObject);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameManager.Instance.GetPowerUp(PowerUpType.SplitBall);
        }

        if (Input.GetMouseButtonDown(1))
        {
            GameManager.Instance.GetPowerUp(PowerUpType.MultiBall);
        }
    }
}
