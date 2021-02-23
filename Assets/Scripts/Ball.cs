using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float speed;

    new private Rigidbody rigidbody;
    new private Renderer renderer;

    public Vector3 Velocity
    {
        get { return rigidbody.velocity; }
        set { rigidbody.velocity = value; }
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        rigidbody.velocity = transform.up * speed;
    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Pad"))
        {
            OnPadHit(other);
        }

        rigidbody.velocity = rigidbody.velocity.normalized * speed;
    }

    private void OnPadHit(Collision pad)
    {
        var dx = (pad.transform.position.x - transform.position.x) / pad.collider.bounds.extents.x;
        var angle = dx * 45; // [-45, 45]
        rigidbody.velocity += Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.up * speed * 2f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            var angle = Vector3.Angle(rigidbody.velocity, Vector3.right);
            print(angle);
        }
    }

    private void OnBecameInvisible()
    {
        GameManager.Instance.RemoveBall(this);
    }
}
