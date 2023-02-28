using System.Collections.Generic;
using UnityEngine;

public class RoadPool : ObjectPool<Road>
{
    [SerializeField] private List<Road> _prefabs;

    protected override Road CreateObject()
    {
        Road randomPrefab = _prefabs[Random.Range(0, _prefabs.Count)];
        return Instantiate(randomPrefab, transform);
    }
}
