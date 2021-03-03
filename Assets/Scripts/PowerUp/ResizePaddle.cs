using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResizePaddle", menuName = "Breakout/Power Ups/Resize Paddle", order = 0)]
public class ResizePaddle : PowerUp
{
    [Range(-1, 1)]
    [SerializeField] private float additionalSize = 0;

    public override void Apply()
    {
        var paddle = GameManager.Instance.Paddle;
        paddle.transform.localScale += Vector3.right * additionalSize;
    }
}