using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Spawn Points")]
    [SerializeField] private Transform ballSpawnPoint;
    [SerializeField] private Transform paddleSpawnPoint;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI text;

    [Header("Prefabs")]
    [SerializeField] private Ball ballPrefab;
    [SerializeField] private Paddle paddlePrefab;
    [SerializeField] private PowerUpItem powerUpPrefab;

    [Header("Settings")]
    [Range(0, 1)]
    [SerializeField] private float powerUpDropRate;

    [Header("Presets")]
    [SerializeField] private List<PowerUp> powerUps;

    public List<Ball> Balls { get; private set; }
    public Paddle Paddle { get; private set; }

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
        Balls = new List<Ball>() { ball };

        Paddle = Instantiate(paddlePrefab, paddleSpawnPoint);
    }

    public Ball CreateBall(Vector3 position, float angle = -45f)
    {
        return Instantiate(ballPrefab, position, Quaternion.AngleAxis(angle, Vector3.forward));
    }

    public void RemoveBall(Ball ball)
    {
        Balls.Remove(ball);
        Destroy(ball.gameObject);
    }

    public void DropPowerUp(Vector3 position)
    {
        var dropChance = Random.Range(0f, 1f);
        if (dropChance > powerUpDropRate)
            return;

        var dropRange = 100 * dropChance / powerUpDropRate;

        PowerUp.DropRate dropType;
        if (dropRange < 40) dropType = PowerUp.DropRate.High;
        else if (dropRange < 70) dropType = PowerUp.DropRate.Average;
        else if (dropRange < 90) dropType = PowerUp.DropRate.Low;
        else dropType = PowerUp.DropRate.VeryLow;

        var possiblePowerUps = powerUps.FindAll(p => p.Rate == dropType);
        if (possiblePowerUps.Count > 0)
        {
            var powerUp = Instantiate(powerUpPrefab, position, Quaternion.identity);
            var index = Random.Range(0, possiblePowerUps.Count);
            powerUp.PowerUp = possiblePowerUps[index];
        }

    }
}
