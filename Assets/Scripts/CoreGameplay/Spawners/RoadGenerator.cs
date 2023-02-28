using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private RoadPool _pool;

    [SerializeField] private int _startPlayerOffset = 10;
    [SerializeField] private int _playerOffset = 10;
    [SerializeField] private float _roadLength = 100;
    [SerializeField] private int _createCount = 6;

    private List<Road> _activeRoads = new List<Road>();

    private float _spawnPosition = 0;

    private void Start()
    {
        _spawnPosition = _player.position.z + _roadLength / 2 - _startPlayerOffset;
    }

    private void Update()
    {
        if (_player.position.z + _roadLength + _playerOffset >= _spawnPosition)
        {
            for (int i = 0; i < _createCount; i++)
            {
                SpawnRoad();
            }
        }
    }

    private void SpawnRoad()
    {
        Road nextRoad = _pool.Get();
        //nextRoad.transform.position = transform.forward * _spawnPosition;
        nextRoad.transform.position = new Vector3(nextRoad.transform.position.x, nextRoad.transform.position.y, _spawnPosition);

        _activeRoads.Add(nextRoad);
        _spawnPosition += _roadLength;
    }
}