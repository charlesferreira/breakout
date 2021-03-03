using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] private IntVariable startingSize;
    [SerializeField] private IntVariable minSize;
    [SerializeField] private IntVariable maxSize;

    [SerializeField] private Transform bar;
    [SerializeField] private Transform leftEnd;
    [SerializeField] private Transform rightEnd;

    private float baseSize;
    private new Rigidbody rigidbody;

    private int _size;
    public int Size
    {
        get { return _size; }
        set { SetSize(value); }
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        baseSize = bar.transform.localScale.y / startingSize.value;
        Size = startingSize.value;
    }

    private void Update()
    {
        var position = transform.position;
        position.x = Camera.main.ScreenToWorldPoint(Vector3.right * Input.mousePosition.x).x;
        position.x = Mathf.Min(25.5f - Size * baseSize, Mathf.Abs(position.x)) * Mathf.Sign(position.x);
        rigidbody.MovePosition(position);
    }

    private void Resize()
    {
        var newSize = baseSize * Size;
        bar.transform.localScale = new Vector3(bar.transform.localScale.x, newSize, bar.transform.localScale.z);
        leftEnd.transform.localPosition = Vector3.left * newSize;
        rightEnd.transform.localPosition = Vector3.right * newSize;
    }

    private void SetSize(int size)
    {
        _size = Mathf.Clamp(size, minSize.value, maxSize.value);
        Resize();
    }
}
