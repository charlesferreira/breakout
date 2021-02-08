using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] int hits = 1;
    [SerializeField] int points = 100;

    private void OnCollisionEnter(Collision other)
    {
        hits -= 1;
        if (hits <= 0)
        {
            Destroy(gameObject);
        }
    }
}
