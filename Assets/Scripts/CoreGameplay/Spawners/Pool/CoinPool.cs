using System.Collections.Generic;
using UnityEngine;

public class CoinPool : ObjectPool<Coin>
{
    [SerializeField] private List<Coin> _prefabs;

    protected override Coin CreateObject()
    {
        Coin randomPrefab = _prefabs[Random.Range(0, _prefabs.Count)];
        return Instantiate(randomPrefab, transform);
    }
}
