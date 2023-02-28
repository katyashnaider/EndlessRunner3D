using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Removal : MonoBehaviour
{
    [SerializeField] private ObstaclePool _obstacleSpawn;
    [SerializeField] private CoinPool _coinSpawn;
    [SerializeField] private RoadPool _roadGenerator;
    [SerializeField] private LandscapePool _landscapeSpawn;
    [SerializeField] private BonusProtectionAndAccelerationPool _bonusProtectionAndAccelerationSpawn;
    [SerializeField] private BonusDoubleCoinPool _bonusDoublingCoinSpawn;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Obstacle obstacle))
            _obstacleSpawn.Put(obstacle);

        if (collider.TryGetComponent(out Coin coin))
            _coinSpawn.Put(coin);
        
        if (collider.TryGetComponent(out Road road))
            _roadGenerator.Put(road);

        if (collider.TryGetComponent(out Landscape landscape))
            _landscapeSpawn.Put(landscape);

        if (collider.TryGetComponent(out BonusProtectionAndAcceleration bonusProtectionAndAcceleration))
            _bonusProtectionAndAccelerationSpawn.Put(bonusProtectionAndAcceleration);

        if (collider.TryGetComponent(out BonusDoubleCoin bonusDoublingCoin))
            _bonusDoublingCoinSpawn.Put(bonusDoublingCoin);
        
    }
}