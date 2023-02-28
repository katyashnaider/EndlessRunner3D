using UnityEngine;

public class Coin : MonoBehaviour
{
    public const int Amount = 1;

    private void Start()
    {
        TurnAnimation.StartAnimation(transform);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}