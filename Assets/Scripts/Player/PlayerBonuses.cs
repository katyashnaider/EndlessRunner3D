using System;
using System.Collections;
using UnityEngine;

public class PlayerBonuses : MonoBehaviour
{
    [SerializeField] private float _delayTime = 5;

    private PlayerMovement _playerMovement;
    private PlayerCoinCollector _playerCoinCollector;
    private LayerMask _obstacleMask = 1 << 7;
    private LayerMask _playerMask = 1 << 8;

    private int _speedMultiplier = 2;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerCoinCollector = GetComponent<PlayerCoinCollector>();
    }

    private void OnEnable()
    {

        _playerMovement.SpeedChanged += OnSpeedChanged;
    }

    private void OnDisable()
    {
        _playerMovement.SpeedChanged -= OnSpeedChanged;
    }

    public void EnableProtection()
    {
        StartCoroutine(Protect());
    }

    public void EnableAcceleration(float speed)
    {
        StartCoroutine(Accelerate(speed));
    }

    private void OnSpeedChanged(float speed)
    {
        StartCoroutine(Accelerate(speed));
    }

    public void EnableDoublePrice()
    {
        StartCoroutine(DoublePrice());
    }

    private IEnumerator Protect()
    {
        Physics.IgnoreLayerCollision(8, 7, true);

        yield return new WaitForSeconds(_delayTime);

        Physics.IgnoreLayerCollision(8, 7, false);
    }

    private IEnumerator Accelerate(float speed)
    {
        float startSpeed = _playerMovement.Speed;
        _playerMovement.SetSpeed(speed * _speedMultiplier);

        yield return new WaitForSeconds(_delayTime);

        _playerMovement.SetSpeed(startSpeed);
    }


    private IEnumerator DoublePrice()
    {
        _playerCoinCollector.EnableDoubleCoins();

        yield return new WaitForSeconds(_delayTime);

        _playerCoinCollector.DisableDoubleCoins();
    }
}
