using UnityEngine;

public enum PowerUpType { SplitBall, MultiBall }

[CreateAssetMenu(fileName = "PowerUpPreset", menuName = "Breakout/Power Up Preset", order = 0)]
public class PowerUpPreset : ScriptableObject
{
    public PowerUpType type;
    public Sprite sprite;
}