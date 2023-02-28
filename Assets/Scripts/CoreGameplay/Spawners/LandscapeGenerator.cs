using System.Collections.Generic;
using UnityEngine;

public class LandscapeGenerator : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private LandscapePool _pool;
    [SerializeField] private Transform[] _landscapeSpawnPoints;

    [SerializeField] private float _landscapeLength = 100f;
    [SerializeField] private int _createCount = 1;
    [SerializeField] private float _spawnOffset = 10f;
    [SerializeField] private float _playerOffset = 10f;

    private List<Landscape> _activeLandscapes = new List<Landscape>();
    private float _spawnPosition = 0f;

    private void Start()
    {
        _spawnPosition = _player.position.z + _landscapeLength + _spawnOffset;

        for (int i = 0; i < _landscapeSpawnPoints.Length; i++)
        {
            float landscapePositionZ = _spawnPosition + (_landscapeLength + _spawnOffset) * i;
            _landscapeSpawnPoints[i].position = new Vector3(_landscapeSpawnPoints[i].position.x, _landscapeSpawnPoints[i].position.y, landscapePositionZ);
        }
    }

    private void Update()
    {
        if (_player.position.z + _landscapeLength + _playerOffset >= _spawnPosition)
        {
            for (int i = 0; i < _createCount; i++)
            {
                SpawnLandscape();
            }
        }
    }

    private void SpawnLandscape()
    {
        int nextSpawnIndex = _activeLandscapes.Count % _landscapeSpawnPoints.Length;
        Transform nextSpawnPoint = _landscapeSpawnPoints[nextSpawnIndex];

        Landscape nextLandscape = _pool.Get();
        float newLandscapePositionZ = _spawnPosition + (_landscapeLength / 2f) + (_landscapeLength + _spawnOffset) * nextSpawnIndex;
        nextLandscape.transform.position = new Vector3(nextSpawnPoint.position.x, nextLandscape.transform.position.y, newLandscapePositionZ);
        _spawnPosition += _landscapeLength;

        _activeLandscapes.Add(nextLandscape);
    }
}
