using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class BonusSpawnerBase<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private ObjectPool<T> _pool;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Transform _player;
    [SerializeField] private float _delaySpawn = 20;
    [SerializeField] private float _offset = 80;
    [SerializeField] private float _radius = 1.5f;

    private void Start()
    {
        StartCoroutine(SpawnDelayed());
    }

    private void Spawn()
    {
        T spawned;

        int spawnPointNumber = Random.Range(0, _spawnPoints.Length);

        Vector3 spawnPosition = _spawnPoints[spawnPointNumber].position;
        spawnPosition.z = _player.position.z + _offset;

        while (CheckColliders(spawnPosition, _radius))
        {
            spawnPosition.x += Random.Range(-1f, 1f);
            spawnPosition.z += Random.Range(-1f, 1f);
        }

        spawned = _pool.Get();

        spawned.transform.position = new Vector3(spawnPosition.x, transform.position.y, spawnPosition.z); ;
    }

    private bool CheckColliders(Vector3 position1, float radius)
    {
        Collider[] colliders1 = Physics.OverlapSphere(position1, radius);

        foreach (var collider in colliders1)
        {
            if (collider.TryGetComponent(out Obstacle obstacle) || collider.TryGetComponent(out Coin coin)  || collider.TryGetComponent(out BonusDoublingCoin bonusDoublingCoin) || collider.TryGetComponent(out BonusProtectionAndAcceleration bonusProtectionAndAcceleration))
                return true;
        }         

        return false;
    }

    private IEnumerator SpawnDelayed()
    {
        while (true)
        {
            yield return new WaitForSeconds(_delaySpawn);

            Spawn();
        }
    }
}