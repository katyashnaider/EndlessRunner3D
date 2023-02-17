using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private ObjectPool _pool;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Transform _player;

    [SerializeField] private float _coinsOffset = 50;
    [SerializeField] private float _delaySpawn = 1;
    [SerializeField] private float _delayRemove = 1;
    [SerializeField] private int _minCoinsPerSpawn = 3;
    [SerializeField] private int _maxCoinsPerSpawn = 15;
    [SerializeField] private int _randomYMin = 1;
    [SerializeField] private int _randomYMax = 3;

    private void Start()
    {
        StartCoroutine(SpawnDelayed());
    }

    //private GameObject[] Spawn()
    //{
    //    int coinsToSpawn = Random.Range(3, _coinsPerSpawn + 1);
    //    GameObject[] spawned = new GameObject[coinsToSpawn];

    //    float zOffset = 0;

    //    for (int i = 0; i < coinsToSpawn; i++)
    //    {
    //        int spawnPointNumber = Random.Range(0, _spawnPoints.Length);
    //        GameObject coin = _pool.Get();

    //        Vector3 spawnPosition = _spawnPoints[spawnPointNumber].position;

    //        float distanceBetweenCoins = 2.0f;
    //        spawnPosition.z = _player.position.z + _coinsOffset + i * distanceBetweenCoins;

    //        coin.transform.position = spawnPosition;
    //        coin.transform.rotation = transform.rotation;

    //        spawned[i] = coin;

    //        zOffset += 1.0f;
    //    }

    //    return spawned;
    //}

    private GameObject[] Spawn()
    {
        int coinsToSpawn = Random.Range(_minCoinsPerSpawn, _maxCoinsPerSpawn + 1);
        int spawnLineIndex = Random.Range(0, _spawnPoints.Length);
        float zOffset = 0;

        GameObject[] spawned = new GameObject[coinsToSpawn];
        Vector3 spawnLinePosition = _spawnPoints[spawnLineIndex].position;

        for (int i = 0; i < coinsToSpawn; i++)
        {
            GameObject coin = _pool.Get();

            float distanceBetweenCoins = 2.0f;
            Vector3 spawnPosition = spawnLinePosition;
            spawnPosition.z = _player.position.z + _coinsOffset + i * distanceBetweenCoins;

            coin.transform.position = spawnPosition;
            coin.transform.rotation = transform.rotation;

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
            GameObject[] spawnedCoins = Spawn();

            foreach (GameObject coin in spawnedCoins)
            {
                StartCoroutine(Remove(coin));
            }

            yield return wait;
        }
    }

    private IEnumerator Remove(GameObject coin)
    {
        yield return new WaitForSeconds(_delayRemove);

        _pool.Put(coin);
    }
}
