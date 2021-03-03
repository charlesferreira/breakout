using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpItem : MonoBehaviour
{
    private PowerUp _powerUp;
    public PowerUp PowerUp
    {
        get { return _powerUp; }
        set { SetPowerUp(value); }
    }

    private SpriteRenderer spriteRenderer;
    private bool applied = false;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Destroy(gameObject, 3);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (applied) return;
        applied = true;

        Destroy(gameObject);
        PowerUp.Apply();
    }

    private void SetPowerUp(PowerUp value)
    {
        _powerUp = value;
        spriteRenderer.sprite = value.Icon;
    }
}
