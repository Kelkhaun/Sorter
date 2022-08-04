using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Pusher : MonoBehaviour
{
    [SerializeField] Train _train;
    [SerializeField] private List<Transform> _points;
    [Range(1, 250)][SerializeField] float _throwForce;

    private Collider _collider;
    private int _targetPoint;
    private int _firstPoint = 0;

    private void OnEnable()
    {
        _train.TrainIsFull += OnTrainFull;
    }

    private void OnDisable()
    {
        _train.TrainIsFull -= OnTrainFull;
    }

    private void Start()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Passenger passenger))
        {
            _targetPoint = Random.Range(_firstPoint, _points.Count);
            passenger.PushToPoint(_points[_targetPoint].position, _throwForce);
        }
    }

    private void OnTrainFull()
    {
        _collider.enabled = false;
    }
}
