using System;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    [SerializeField] private float _delta;

    private bool _isDraing = false;
    private Vector3 _startTouch;

    public event Action<Vector2> Swiped;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startTouch = Input.mousePosition;
            _isDraing = true;
        }

        if (Input.GetMouseButton(0) && _isDraing)
        {
            Vector2 swipeDirection = Input.mousePosition - _startTouch;

            if (Mathf.Abs(swipeDirection.x) >= _delta)
            {
                if (swipeDirection.x > 0)
                {
                    Swiped?.Invoke(Vector2.right);
                    _isDraing = false;
                }
                else
                {
                    Swiped?.Invoke(Vector2.left);
                    _isDraing = false;
                }
            }

            //прыжок и проскальзывание вниз
            //if (Mathf.Abs(swipeDirection.y) >= _delta)
            //{
            //    if (swipeDirection.y > 0)
            //    {
            //        Swiped?.Invoke(Vector2.right); //заменить на прыжок
            //        _isDraing = false;
            //    }
            //    else
            //    {
            //        Swiped?.Invoke(Vector2.left); //заменить на проскальзывание вниз
            //        _isDraing = false;
            //    }
            //}
        }
    }
}


