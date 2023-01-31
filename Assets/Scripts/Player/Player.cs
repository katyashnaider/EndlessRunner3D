using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int _wallet = 0;

    public Action Died;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            OnDied();            
        }
    }

    public void ApplyCoin(int money)
    {
        _wallet += money;
    }

    private void OnDied()
    {
        Died?.Invoke();       
    }
}
