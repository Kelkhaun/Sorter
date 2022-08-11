using System;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

public class Vagon : MonoBehaviour
{
    [SerializeField] private int _vagonCapacity;
    [SerializeField] private bool _isBlue;
    [SerializeField] private bool _isRed;
    [SerializeField] private VagonExpander _vagonExpander;
    [SerializeField] Transform[] _targetPoints;

    private List<Passenger> _passengers = new List<Passenger>();
    private Train _train;
    private Passenger _currentPassenger;
    private Door[] _doors;
    private bool _IsFullVagon;
    private int _passengersNumberInVagon = 0;
    private float _endValue1 = -0.8f;
    private float _endValue2 = -0.65f;
    private float _endValue3 = -0.823f;
    private float _endValue4 = -0.631f;
    private int _secondVagon = 2;
    private float _duration = 0.2f;

    public bool IsBlue => _isBlue;
    public bool IsRed => _isRed;
    public int VagonCapacity => _vagonCapacity;

    public event Action VagonIsFull;
    public event Action<int, int, Passenger, Vagon> PassengerEnteredTheVagon;

    private void Start()
    {
        _train = GetComponentInParent<Train>();
        _doors = GetComponentsInChildren<Door>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Passenger passenger) && passenger.IsInTrain == false)
        {
            int randomPosition = UnityEngine.Random.Range(0, _targetPoints.Length);
            _passengers.Add(passenger);
            passenger.EnableBeingOnTheTrain();
            StartCoroutine(passenger.MoveToTarget(_targetPoints[randomPosition], _duration));
            passenger.transform.parent = this.gameObject.transform;
            _currentPassenger = passenger;
            AddPassanger();

            if (_passengersNumberInVagon == _vagonCapacity && _IsFullVagon == false)
            {
                MakeKinematicsOfPassengers();

                _IsFullVagon = true;
                VagonIsFull?.Invoke();
                _vagonExpander.ExpandTheVagon();

                if (_train.CurrentVagon == _secondVagon)
                    _vagonExpander.NarrowVagon();

                CloseDoors();
            }
        }
    }

    private void MakeKinematicsOfPassengers()
    {
        for (int i = 0; i < _passengers.Count; i++)
        {
            StartCoroutine(_passengers[i].MakeIsKinematic());
        }
    }

    private void CloseDoors()
    {
        if (IsBlue == true)
            StartCoroutine(CloseDoor(_endValue1, _endValue2));
        else
            StartCoroutine(CloseDoor(_endValue3, _endValue4));
    }

    private void AddPassanger()
    {
        if (_isBlue == true && _currentPassenger.IsBlue)
        {
            _passengersNumberInVagon++;
            PassengerEnteredTheVagon?.Invoke(_passengersNumberInVagon, _vagonCapacity, _currentPassenger, this);
        }
        else if (_isBlue == true && _currentPassenger.IsRed)
        {
            PassengerEnteredTheVagon?.Invoke(_passengersNumberInVagon, _vagonCapacity, _currentPassenger, this);
        }
        else if (_isRed == true && _currentPassenger.IsRed)
        {
            _passengersNumberInVagon++;
            PassengerEnteredTheVagon?.Invoke(_passengersNumberInVagon, _vagonCapacity, _currentPassenger, this);
        }
        else if (_isRed == true && _currentPassenger.IsBlue)
        {
            PassengerEnteredTheVagon?.Invoke(_passengersNumberInVagon, _vagonCapacity, _currentPassenger, this);
        }
    }
    public IEnumerator OpenDoor()
    {
        float delay = 2.1f;
        WaitForSeconds waitForSeconds = new WaitForSeconds(delay);

        yield return waitForSeconds;
        float endValue1 = 0.081f;
        float endValue2 = -1.916f;
        float duration = 0.3f;

        for (int i = 0; i < _doors.Length; i++)
        {
            if (_doors[i].IsRight == true)
                _doors[i].transform.DOLocalMoveX(endValue1, duration);

            if (_doors[i].IsLeft == true)
                _doors[i].transform.DOLocalMoveX(endValue2, duration);
        }
    }

    private IEnumerator CloseDoor(float endValue1, float endValue2)
    {
        float delay = 0.43f;
        WaitForSeconds waitForSeconds = new WaitForSeconds(delay);

        yield return waitForSeconds;
        float duration = 0.1f;

        for (int i = 0; i < _doors.Length; i++)
        {
            if (_doors[i].IsRight == true)
                _doors[i].transform.DOLocalMoveX(endValue1, duration);

            if (_doors[i].IsLeft == true)
                _doors[i].transform.DOLocalMoveX(endValue2, duration);
        }
    }
}
