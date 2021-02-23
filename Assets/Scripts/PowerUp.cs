using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private PowerUpPreset preset;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        Destroy(gameObject, 3);
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = preset.sprite;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        GameManager.Instance.GetPowerUp(preset.type);
    }
}
