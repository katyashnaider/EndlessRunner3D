using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    private bool _isDraging = false;
    private Vector2 _startTouch, _swipeDelta;

    public static bool Tap { get; private set; }
    public static bool SwipeLeft { get; private set; }
    public static bool SwipeRight { get; private set; }
    public static bool SwipeUp { get; private set; }
    public static bool SwipeDown { get; private set; }

    private void Update()
    {
        Tap = SwipeDown = SwipeUp = SwipeLeft = SwipeRight = false;

        #region ПК-версия
        if (Input.GetMouseButtonDown(0))
        {
            Tap = true;
            _isDraging = true;
            _startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isDraging = false;
            Reset();
        }
        #endregion

        #region Мобильная версия
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                Tap = true;
                _isDraging = true;
                _startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                _isDraging = false;
                Reset();
            }
        }
        #endregion

        //Просчитать дистанцию
        _swipeDelta = Vector2.zero;

        if (_isDraging)
        {
            if (Input.touches.Length < 0)
                _swipeDelta = Input.touches[0].position - _startTouch;
            else if (Input.GetMouseButton(0))
                _swipeDelta = (Vector2)Input.mousePosition - _startTouch;
        }

        //Проверка на пройденность расстояния
        if (_swipeDelta.magnitude > 100)
        {
            //Определение направления
            float x = _swipeDelta.x;
            float y = _swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {

                if (x < 0)
                    SwipeLeft = true;
                else
                    SwipeRight = true;
            }
            else
            {

                if (y < 0)
                    SwipeDown = true;
                else
                    SwipeUp = true;
            }

            Reset();
        }

    }

    private void Reset()
    {
        _startTouch = _swipeDelta = Vector2.zero;
        _isDraging = false;
    }
}