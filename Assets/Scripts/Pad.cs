using UnityEngine;

public class Pad : MonoBehaviour
{
    private new Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var position = transform.position;
        position.x = Camera.main.ScreenToWorldPoint(Vector3.right * Input.mousePosition.x).x;
        rigidbody.MovePosition(position);
    }
}
