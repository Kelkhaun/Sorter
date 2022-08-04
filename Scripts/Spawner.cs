using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Train _train;
    [SerializeField] private Passenger[] _passengers;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private bool isSpawn;
    [Space(20)][Range(1, 5)][SerializeField] private float _timeBetweenSpawn;
    [SerializeField] private int _minimumPassengerCount;
    [SerializeField] private int _maximumPassangeyCount;

    private Coroutine _spawnPassenger;
    private int _chanceToSpawnOppositePassenger = 50;
    private int _currentPassenger = 0;
    private int _onePassenger = 1;
    private int _minimumValueForRandom = 0;


    private void OnEnable()
    {
        _train.VagonChanged += ChangePassenger;
        _train.TrainIsFull += StopSpawn;
    }

    private void OnDisable()
    {
        _train.VagonChanged -= ChangePassenger;
        _train.TrainIsFull -= StopSpawn;
    }

    private void Start()
    {
        _spawnPassenger = StartCoroutine(SpawnEnemy());
    }

    private void ChangePassenger()
    {
        if (_currentPassenger != _passengers.Length - _onePassenger)
            _currentPassenger++;
    }

    private void StopSpawn()
    {
        isSpawn = false;
        StopCoroutine(_spawnPassenger);
    }

    private IEnumerator SpawnEnemy()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_timeBetweenSpawn);

        while (isSpawn == true)
        {
            int enemyCount = Random.Range(_minimumPassengerCount, _maximumPassangeyCount);
            int randomPosition = Random.Range(_minimumValueForRandom, _spawnPoints.Length);

            for (int i = 0; i < enemyCount; i++)
            {
                Instantiate(_passengers[_currentPassenger], _spawnPoints[i].position, Quaternion.identity, transform);
            }

            SpawnOppositePassenger(randomPosition);

            yield return waitForSeconds;
        }
    }

    private void SpawnOppositePassenger(int randomPosition)
    {
        float maximuValue = 100;
        int firstPassenger = 0;

        if (Random.Range(_minimumValueForRandom, maximuValue) < _chanceToSpawnOppositePassenger)
        {
            if (_currentPassenger == firstPassenger)
                Instantiate(_passengers[_currentPassenger + _onePassenger], _spawnPoints[randomPosition].position, Quaternion.identity, transform);
            else
                Instantiate(_passengers[_currentPassenger - _onePassenger], _spawnPoints[randomPosition].position, Quaternion.identity, transform);
        }
    }
}
