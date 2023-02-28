using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private ObstaclePool _pool;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Transform _player;
    [SerializeField] private AnimationCurve _spawnDelayRange;

    [SerializeField] private float _obstaclesOffset = 80;

    public float DelaySpawn => _spawnDelayRange.Evaluate(Timer.PlayTime);

    private void Start()
    {
        StartCoroutine(SpawnDelayed());
    }

    private Obstacle Spawn()
    {
        int spawnPointNumber = Random.Range(0, _spawnPoints.Length);
        Obstacle spawned = _pool.Get();

        Vector3 spawnPosition = _spawnPoints[spawnPointNumber].position;
        spawnPosition.z = _player.position.z + _obstaclesOffset;
        spawned.transform.position = new Vector3(spawnPosition.x, transform.position.y, spawnPosition.z);

        return spawned;
    }

    private IEnumerator SpawnDelayed()
    {
        while (true)
        {
            Spawn();

            yield return new WaitForSeconds(DelaySpawn);
        }
    }  
}