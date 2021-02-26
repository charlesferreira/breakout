using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : ScriptableObject
{
    public enum DropRate { VeryLow, Low, Average, High }

    [SerializeField] private Sprite icon;

    [SerializeField] private DropRate dropRate;

    public Sprite Icon { get { return icon; } }

    public abstract void Apply();
}