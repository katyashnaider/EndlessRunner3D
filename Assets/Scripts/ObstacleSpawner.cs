using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private ObjectPool _pool;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Transform _player;

    [SerializeField] private float _obstaclesOffset = 80;
    [SerializeField] private float _delaySpawn = 1;
    [SerializeField] private float _delayRemove = 1;

    private void Start()
    {
        StartCoroutine(SpawnDelayed());
    }

    private GameObject Spawn()
    {
        int spawnPointNumber = Random.Range(0, _spawnPoints.Length);
        GameObject spawned = _pool.Get();

        Vector3 spawnPosition = _spawnPoints[spawnPointNumber].position;

        spawnPosition.z = _player.position.z + _obstaclesOffset;

        spawned.transform.position = spawnPosition;
        spawned.transform.rotation = transform.rotation;

        return spawned;
    }

    private IEnumerator SpawnDelayed()
    {
        var wait = new WaitForSeconds(_delaySpawn);

        while (true)
        {
            StartCoroutine(Remove(Spawn()));

            yield return wait;
        }
    }

    private IEnumerator Remove(GameObject obstacle)
    {
        var wait = new WaitForSeconds(_delayRemove);

        yield return wait;

        _pool.Put(obstacle);
    }
}