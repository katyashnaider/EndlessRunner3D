using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private CoinPool _pool;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Transform _player;

    [SerializeField] private float _coinsOffset = 50;
    [SerializeField] private float _delaySpawn = 1;
    [SerializeField] private float _distanceBetweenCoins = 2f;
    [SerializeField] private int _minCoinsPerSpawn = 3;
    [SerializeField] private int _maxCoinsPerSpawn = 15;

    private void Start()
    {
        StartCoroutine(SpawnDelayed());
    }

    private Coin[] Spawn()
    {
        int coinsToSpawn = Random.Range(_minCoinsPerSpawn, _maxCoinsPerSpawn + 1);
        int spawnLineIndex = Random.Range(0, _spawnPoints.Length);
        float zOffset = 0;

        Coin[] spawned = new Coin[coinsToSpawn];
        Vector3 spawnLinePosition = _spawnPoints[spawnLineIndex].position;
        Vector3 spawnPosition = spawnLinePosition;

        for (int i = 0; i < coinsToSpawn; i++)
        {
            Coin coin = _pool.Get();

            spawnPosition.z = _player.position.z + _coinsOffset + i * _distanceBetweenCoins;
            coin.transform.position = new Vector3(spawnPosition.x, transform.position.y, spawnPosition.z);
            spawned[i] = coin;
            zOffset += 1.0f;
        }

        return spawned;
    }

    private IEnumerator SpawnDelayed()
    {
        var wait = new WaitForSeconds(_delaySpawn);

        while (true)
        {
            Spawn();

            yield return wait;
        }
    }
}
