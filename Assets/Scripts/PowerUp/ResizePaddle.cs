using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResizePaddle", menuName = "Breakout/Power Ups/Resize Paddle", order = 0)]
public class ResizePaddle : PowerUp
{
    public enum ResizeMethod { Add, Set };

    [SerializeField] private ResizeMethod method;
    [SerializeField] private int size = 0;

    public override void Apply()
    {
        var paddle = GameManager.Instance.Paddle;
        paddle.Size = method == ResizeMethod.Add ? paddle.Size + size : size;
    }
}