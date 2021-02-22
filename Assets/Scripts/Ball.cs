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

        if (other.gameObject.CompareTag("Pad"))
        {
            OnPadHit(other);
        }
        // else
        // {
        //     rigidbody.velocity = Vector3.Reflect(velocity, other.contacts[0].normal);
        // }
    }


    private void OnPadHit(Collision pad)
    {
        var dx = (pad.transform.position.x - transform.position.x) / pad.collider.bounds.extents.x;
        var angle = dx * 45; // [-45, 45]
        rigidbody.velocity += Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.up * speed * 2f;
    }
}
