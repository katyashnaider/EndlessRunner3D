using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BonusDoublingCoinPool : ObjectPool<BonusDoublingCoin>
{
    [SerializeField] private List<BonusDoublingCoin> _prefabs;

    protected override BonusDoublingCoin CreateObject()
    {
        BonusDoublingCoin randomPrefab = _prefabs[Random.Range(0, _prefabs.Count)];
        return Instantiate(randomPrefab, transform);
    }
}