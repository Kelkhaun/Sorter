using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Train : MonoBehaviour
{
    [SerializeField] private Vagon[] _vagons;

    private int _currentVagon = 0;
    private float _firstVagon = 0;
    private float _xOffset1 = 6.33f;
    private float _xOffset2 = 18.2f;
    private float _duration1 = 2.5f;
    private float _duration2 = 2f;
    private int _oneVagon = 1;

    public int CurrentVagon => _currentVagon;

    public event Action VagonChanged;
    public event Action TrainIsFull;

    private void OnEnable()
    {
        for (int i = 0; i < _vagons.Length; i++)
        {
            _vagons[i].VagonIsFull += MoveVagon;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _vagons.Length; i++)
        {
            _vagons[i].VagonIsFull -= MoveVagon;
        }
    }

    private void MoveVagon()
    {
        if (_currentVagon != _vagons.Length - _oneVagon)
        {
            StartCoroutine(Move(_xOffset1, _duration1));
            StartCoroutine(_vagons[_currentVagon + _oneVagon].OpenDoor());
            VagonChanged?.Invoke();
            _currentVagon++;
        }
        else
        {
            StartCoroutine(Move(_xOffset2, _duration2));
            TrainIsFull?.Invoke();
        }
    }

    private IEnumerator Move(float xOffset, float duration)
    {
        if (_currentVagon != _firstVagon)
            _currentVagon++;

        float targetPosition = transform.position.x - xOffset;

        float delay = 0.4f;
        WaitForSeconds waitForSeconds = new WaitForSeconds(delay);

        yield return waitForSeconds;

        transform.DOMoveX(targetPosition, duration).SetAutoKill(true);
    }
}
