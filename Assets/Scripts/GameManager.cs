using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Spawn Points")]
    [SerializeField] private Transform ballSpawnPoint;
    [SerializeField] private Transform padSpawnPoint;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI text;

    [Header("Prefabs")]
    [SerializeField] private Ball ballPrefab;
    [SerializeField] private Pad padPrefab;
    [SerializeField] private PowerUpItem powerUpPrefab;

    [Header("Settings")]
    [Range(0, 1)]
    [SerializeField] private float powerUpDropRate;

    [Header("Presets")]
    [SerializeField] private List<PowerUp> powerUps;

    public List<Ball> Balls { get; private set; }
    public Pad Pad { get; private set; }

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

        Pad = Instantiate(padPrefab, padSpawnPoint);
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

        print("DROP");

        var dropRange = 100 * dropChance / powerUpDropRate;

        PowerUp.DropRate dropType;
        if (dropRange < 40) dropType = PowerUp.DropRate.High;
        else if (dropRange < 70) dropType = PowerUp.DropRate.Average;
        else if (dropRange < 90) dropType = PowerUp.DropRate.Low;
        else dropType = PowerUp.DropRate.VeryLow;

        switch (dropType)
        {
            case PowerUp.DropRate.High:
                print("HIGH: " + dropType);
                break;
            case PowerUp.DropRate.Average:
                print("AVERAGE: " + dropType);
                break;
            case PowerUp.DropRate.Low:
                print("LOW: " + dropType);
                break;
            case PowerUp.DropRate.VeryLow:
                print("VERY LOW: " + dropType);
                break;
        }

        var possiblePowerUps = powerUps.FindAll(p => p.Rate == dropType);
        if (possiblePowerUps.Count > 0)
        {
            var powerUp = Instantiate(powerUpPrefab, position, Quaternion.identity);
            var index = Random.Range(0, possiblePowerUps.Count);
            powerUp.PowerUp = possiblePowerUps[index];
        }

    }
}
