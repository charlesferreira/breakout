using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpItem : MonoBehaviour
{
    private PowerUp _powerUp;
    public PowerUp PowerUp
    {
        get { return _powerUp; }
        set
        {
            _powerUp = value;
            spriteRenderer.sprite = value.Icon;
        }
    }

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Destroy(gameObject, 3);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        PowerUp.Apply();
    }
}
