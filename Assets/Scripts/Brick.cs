using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private int hits = 1;
    [SerializeField] private int points = 100;

    [SerializeField] private Material hitMaterial;

    new private Renderer renderer;
    private Material originalMaterial;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        originalMaterial = renderer.sharedMaterial;
    }

    private void OnCollisionEnter(Collision other)
    {
        hits -= 1;
        if (hits <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            renderer.material = hitMaterial;
            Invoke("ResetMaterial", .1f);
        }
    }

    private void ResetMaterial()
    {
        renderer.material = originalMaterial;
    }
}
