using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour, IInput
{
    private bool _isDraging = false;
    private Vector2 _startTouch, _swipeDelta;

    public bool IsTapped { get; set; }
    public bool IsSwipedLeft { get; set; }
    public bool IsSwipedRight { get; set; }
    public bool IsSwipedUp { get; set; }
    public bool IsSwipedDown { get; set; }

    private void Update()
    {
        IsTapped = IsSwipedDown = IsSwipedUp = IsSwipedLeft = IsSwipedRight = false;

        #region ПК-версия
        if (Input.GetMouseButtonDown(0))
        {
            IsTapped = true;
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
                IsTapped = true;
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
                    IsSwipedLeft = true;
                else
                    IsSwipedRight = true;
            }
            else
            {
                if (y < 0)
                    IsSwipedDown = true;
                else
                    IsSwipedUp = true;
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