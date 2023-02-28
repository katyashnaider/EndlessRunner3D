using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BonusProtectionAndAccelerationPool : ObjectPool<BonusProtectionAndAcceleration>
{
    [SerializeField] private List<BonusProtectionAndAcceleration> _prefabs;

    protected override BonusProtectionAndAcceleration CreateObject()
    {
        BonusProtectionAndAcceleration randomPrefab = _prefabs[Random.Range(0, _prefabs.Count)];
        return Instantiate(randomPrefab, transform);
    }
}
