using System;
using UnityEngine;

public class SwipeInput : MonoBehaviour
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
                    Swiped?.Invoke(Vector2.right);
                else
                    Swiped?.Invoke(Vector2.left);

                _isDragging = false;
            }

            if (Mathf.Abs(swipeDirection.y) >= _delta)
            {
                if (swipeDirection.y > 0)
                    Swiped?.Invoke(Vector2.up);
                else
                    Swiped?.Invoke(Vector2.down);

                _isDragging = false;
            }
        }
    }
}


