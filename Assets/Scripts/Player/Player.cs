using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action Died;
    public event Action GameOver;
    public event Action<int> CoinsAmountChanged;

    private void Start()
    {
        CoinsAmountChanged?.Invoke(Characteristics.Coins);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Obstacle _))
        {
            OnDied();            
        }
    }

    public void ApplyCoin(int amount)
    {
        CoinsAmountChanged?.Invoke(Characteristics.Coins + amount);
    }

    private void OnDied()
    {
        Died?.Invoke();
        GameOver?.Invoke();
    }
}