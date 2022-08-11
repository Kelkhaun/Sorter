using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    private const float ZeroSpeed = 0;

    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _rotateSpeed;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float verticalMove = Input.GetAxisRaw("Vertical") * _movementSpeed * Time.fixedDeltaTime;
        float horizontalMove = Input.GetAxisRaw("Horizontal") * _rotateSpeed * Time.fixedDeltaTime;
        Vector3 rotation = new Vector3(transform.rotation.x, horizontalMove, transform.rotation.z);

        if (horizontalMove != ZeroSpeed)
            transform.Rotate(rotation);
        else
            _rigidbody.angularVelocity = Vector3.zero;

        if (verticalMove < ZeroSpeed)
            return;

        _rigidbody.MovePosition(transform.position + transform.forward * verticalMove);
    }
}

