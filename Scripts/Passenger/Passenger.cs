using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Passenger : MonoBehaviour
{
    [Range(1,10)][SerializeField] private float _movementSpeed;
    [Range(1, 150)][SerializeField] private float _repulsiveForce;
    [SerializeField] private bool _isBlue;
    [SerializeField] private bool _isRed;

    private Coroutine _move;
    private Rigidbody _rigidbody;
    private bool _isInTrain = false;

    public bool IsRed => _isRed;
    public bool IsBlue => _isBlue;
    public bool IsInTrain => _isInTrain;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _move = StartCoroutine(Move());
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
            StopCoroutine(_move);

        if (collision.gameObject.TryGetComponent(out Passenger passenger) && _isInTrain == true)
        {
            Vector3 direction = passenger.transform.position - transform.position;
            passenger.PushInRandomDirection(direction);
        }
    }

    public void PushToPoint(Vector3 targetPosition, float throwForce)
    {
        Vector3 targetPoint = targetPosition - transform.position;
        _rigidbody.AddForce(targetPoint * throwForce, ForceMode.Acceleration);
    }

    public void EnableBeingOnTheTrain()
    {
        _isInTrain = true;
    }

    public IEnumerator MakeIsKinematic()
    {
        float delay = 0.5f;
        WaitForSeconds waitForSeconds = new WaitForSeconds(delay);

        yield return waitForSeconds;
        _rigidbody.isKinematic = true;
    }


    public void MakeGravity()
    {
        _rigidbody.useGravity = true;
    }

    private void PushInRandomDirection(Vector3 direction)
    {
        _rigidbody.AddForce(direction * _repulsiveForce, ForceMode.Force);
    }

    public IEnumerator MoveToTarget(Transform targetPosition, float movementSpeed)
    {
        while (transform.position != targetPosition.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, movementSpeed * Time.fixedDeltaTime);
            yield return null;
        }
    }

    private IEnumerator Move()
    {
        float zOffset = 9f;
        float delay = 2f;
        WaitForSeconds waitForSeconds = new WaitForSeconds(delay);
        Vector3 targetPosition = new Vector3(_rigidbody.position.x, _rigidbody.position.y, _rigidbody.position.z - zOffset);

        while (transform.position.z > targetPosition.z)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _movementSpeed * Time.fixedDeltaTime);
            yield return null;
        }

        yield return waitForSeconds;
        MakeGravity();
    }
}
