using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResizePad", menuName = "Breakout/Power Ups/Resize Pad", order = 0)]
public class ResizePad : PowerUp
{
    [Range(-1, 1)]
    [SerializeField] private float additionalSize = 0;

    public override void Apply()
    {
        var pad = GameManager.Instance.Pad;
        pad.transform.localScale += Vector3.right * additionalSize;
    }
}