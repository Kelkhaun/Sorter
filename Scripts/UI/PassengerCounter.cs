using UnityEngine;
using TMPro;
using System.Collections;

public class PassengerCounter : MonoBehaviour
{
    [SerializeField] private Vagon _vagon;
    [SerializeField] private Train _train;
    [SerializeField] private PassengerCounter _nextPassengerField;

    private TMP_Text _text;
    private float _delay = 0.7f;

    private void OnEnable()
    {
        _train.TrainIsFull += OnTrainIsFull;
        _vagon.PassengerEnteredTheVagon += AddPassenger;
        _vagon.VagonIsFull += OnVagonIsFull;
    }

    private void OnDisable()
    {
        _train.TrainIsFull -= OnTrainIsFull;
        _vagon.PassengerEnteredTheVagon -= AddPassenger;
        _vagon.VagonIsFull -= OnVagonIsFull;
    }

    private void Start()
    {
        _text = GetComponentInChildren<TMP_Text>();
        _text.text = "0/" + _vagon.VagonCapacity;
    }

    private void OnTrainIsFull()
    {
        StartCoroutine(TurnOffObject());
    }

    private void OnVagonIsFull()
    {
        StartCoroutine(EnableNextPassengerField());
        StartCoroutine(TurnOffObject());
    }

    private void AddPassenger(int passengersNumberInVagon, int vagonCapacity, Passenger currentPassenger, Vagon vagon)
    {
        _text.text = passengersNumberInVagon + "/" + vagonCapacity;
    }

    private IEnumerator EnableNextPassengerField()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_delay);
        yield return waitForSeconds;

        if (_nextPassengerField != null)
            _nextPassengerField.gameObject.SetActive(true);
    }

    private IEnumerator TurnOffObject()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_delay);
        yield return waitForSeconds;

        gameObject.SetActive(false);
    }
}
