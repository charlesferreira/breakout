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
            OnPadHit(other);

        if (other.gameObject.CompareTag("Wall"))
            OnWallHit();


        rigidbody.velocity = rigidbody.velocity.normalized * speed;
    }

    private void OnPadHit(Collision pad)
    {
        var dx = (pad.transform.position.x - transform.position.x) / pad.collider.bounds.extents.x;
        var angle = dx * 45; // [-45, 45]
        rigidbody.velocity += Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.up * speed * 2f;
    }

    private void OnWallHit()
    {
        var angle = Vector3.Angle(Vector3.up, rigidbody.velocity) - 90;
        print(angle);
        if (Mathf.Abs(angle) < 3)
            rigidbody.velocity = Quaternion.AngleAxis(15, Vector3.forward) * rigidbody.velocity;
    }

    private void Update()
    {
        if (Mathf.Abs(transform.position.x) > 27 || Mathf.Abs(transform.position.y) > 15)
            GameManager.Instance.RemoveBall(this);
    }
}
