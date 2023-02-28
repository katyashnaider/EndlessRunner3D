using System.Collections.Generic;
using UnityEngine;

public class LandscapePool : ObjectPool<Landscape>
{
    [SerializeField] private List<Landscape> _prefabs;

    protected override Landscape CreateObject()
    {
        Landscape randomPrefab = _prefabs[Random.Range(0, _prefabs.Count)];
        return Instantiate(randomPrefab, transform);
    }
}
