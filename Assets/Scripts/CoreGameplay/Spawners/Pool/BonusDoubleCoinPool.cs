using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BonusDoubleCoinPool : ObjectPool<BonusDoubleCoin>
{
    [SerializeField] private List<BonusDoubleCoin> _prefabs;

    protected override BonusDoubleCoin CreateObject()
    {
        BonusDoubleCoin randomPrefab = _prefabs[Random.Range(0, _prefabs.Count)];
        return Instantiate(randomPrefab, transform);
    }
}