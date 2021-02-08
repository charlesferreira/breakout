using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float angle;

    new private Rigidbody rigidbody;

    private Vector3 velocity;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rigidbody.velocity = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right * speed;
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = rigidbody.velocity.normalized * speed;
        velocity = rigidbody.velocity;
    }

    private void OnCollisionEnter(Collision other)
    {
        rigidbody.velocity = Vector3.Reflect(velocity, other.contacts[0].normal);
    }
}
