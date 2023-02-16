using System;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    [SerializeField] private float _delta;

    private bool _isDragging = false;
    private Vector3 _startTouch;

    public event Action<Vector2> Swiped;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startTouch = Input.mousePosition;
            _isDragging = true;
        }

        if (Input.GetMouseButton(0) && _isDragging)
        {
            Vector2 swipeDirection = Input.mousePosition - _startTouch;

            if (Mathf.Abs(swipeDirection.x) >= _delta)
            {
                if (swipeDirection.x > 0)
                {
                    Swiped?.Invoke(Vector2.right);
                    _isDragging = false;
                }
                else
                {
                    Swiped?.Invoke(Vector2.left);
                    _isDragging = false;
                }
            }

            //прыжок и проскальзывание вниз
            if (Mathf.Abs(swipeDirection.y) >= _delta)
            {
                if (swipeDirection.y > 0)
                {
                    Swiped?.Invoke(Vector2.up); //прыжок
                    _isDragging = false;
                }
                else
                {
                    Swiped?.Invoke(Vector2.down); //проскальзывание вниз
                    _isDragging = false;
                }
            }
        }
    }
}


