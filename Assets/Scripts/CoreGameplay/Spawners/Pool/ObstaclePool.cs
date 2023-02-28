using System.Collections.Generic;
using UnityEngine;

public class ObstaclePool : ObjectPool<Obstacle>
{
    [SerializeField] private List<Obstacle> _prefabs;

    protected override Obstacle CreateObject()
    {
        Obstacle randomPrefab = _prefabs[Random.Range(0, _prefabs.Count)];
        return Instantiate(randomPrefab, transform);
    }
}
