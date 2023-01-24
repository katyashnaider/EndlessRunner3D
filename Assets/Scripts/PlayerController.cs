using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private int _speed = 3;
    [SerializeField] private float _lineDistance = 4;
    [SerializeField] private float _jumpForce = 3;
    [SerializeField] private float _gravity = 3;

    private CharacterController _characterController;
    private Vector3 _moveDirection;

    private int _lineToMove = 1;
    private int _shiftRight = 2;
    private int _shiftLeft = 0;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (SwipeController.SwipeRight)
        {
            if (_lineToMove < _shiftRight)
                _lineToMove++;
        }

        if (SwipeController.SwipeLeft)
        {
            if (_lineToMove > _shiftLeft)
                _lineToMove--;
        }

        if (SwipeController.SwipeUp)
        {
            if (_characterController.isGrounded)
                Jump();
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (_lineToMove == _shiftLeft)
            targetPosition += Vector3.left * _lineDistance;
        else if (_lineToMove == _shiftRight)
            targetPosition += Vector3.right * _lineDistance;

        transform.position = targetPosition;
    }

    private void FixedUpdate()
    {
        _moveDirection.z = _speed;
        _moveDirection.y += _gravity * Time.fixedDeltaTime;
        Move(_moveDirection);
    }

    private void Jump()
    {
        _moveDirection.y = _jumpForce;
    }

    private void Move(Vector3 direction)
    {
        _characterController.Move(direction * Time.fixedDeltaTime);
    }

}
