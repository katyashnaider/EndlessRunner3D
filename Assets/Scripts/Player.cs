using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Action Died;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            OnDied();
        }
    }

    private void OnDied()
    {
        Died?.Invoke();
    }
}
