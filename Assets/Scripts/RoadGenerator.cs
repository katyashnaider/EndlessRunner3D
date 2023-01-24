using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : PoolManager
{
    [SerializeField] private Transform _player;
    [SerializeField] private ObjectPool _pool;

    [SerializeField] private int _startPlayerOffset = 10;

    private List<GameObject> _activeRoads = new List<GameObject>();

    private float _spawnPosition = 0;
    private float _roadLength = 100;
    private int _startCountRoads = 2;

    private void Start()
    {
        _spawnPosition = CalculateRoadStartPosition() - _startPlayerOffset;

        for (int i = 0; i < _startCountRoads; i++)
            SpawnRoad();
    }

    private void Update()
    {
        if (_player.position.z + _roadLength >= _spawnPosition)
        {
            SpawnRoad();
            DeleteRoad();
        }
    }

    private void SpawnRoad()
    {
        GameObject nextRoad = _pool.Spawn();
        nextRoad.transform.position = transform.forward * _spawnPosition;
        nextRoad.transform.rotation = transform.rotation;

        _activeRoads.Add(nextRoad);
        _spawnPosition += _roadLength;
    }

    private void DeleteRoad()
    {
        _pool.Despawn(_activeRoads[0]);
        _activeRoads.RemoveAt(0);
    }

    private float CalculateRoadStartPosition()
    {
        return _player.position.z + _roadLength / 2;
    }
}