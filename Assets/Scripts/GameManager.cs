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

        Instantiate(padPrefab, padSpawnPoint);
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

        var powerUp = Instantiate(powerUpPrefab, position, Quaternion.identity);
        powerUp.PowerUp = powerUps[0];
    }
}
