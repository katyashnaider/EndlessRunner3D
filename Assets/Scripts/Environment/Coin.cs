using DG.Tweening;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public const int Amount = 1;

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}