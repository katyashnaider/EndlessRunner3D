using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private int _speed = 3;
    [SerializeField] private float _lineDistance = 4;
    [SerializeField] private float _jumpForce = 3;
    [SerializeField] private float _gravity = 3;
    [SerializeField] private SwipeController _swipeController;
    [SerializeField] private GameObject _losePanel;

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
        if (_swipeController.IsSwipedRight)
        {
            if (_lineToMove < _shiftRight)
                _lineToMove++;
        }

        if (_swipeController.IsSwipedLeft)
        {
            if (_lineToMove > _shiftLeft)
                _lineToMove--;
        }

        if (_swipeController.IsSwipedUp)
        {
            if (_characterController.isGrounded)
                Jump();
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (_lineToMove == _shiftLeft)
            targetPosition += Vector3.left * _lineDistance;
        else if (_lineToMove == _shiftRight)
            targetPosition += Vector3.right * _lineDistance;

        if (transform.position == targetPosition)
            return;

        Vector3 difference = targetPosition - transform.position;
        Vector3 moveDirection = difference.normalized * 25 * Time.deltaTime;

        if (moveDirection.sqrMagnitude < difference.sqrMagnitude)
            _characterController.Move(moveDirection);
        else
            _characterController.Move(difference);
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

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.TryGetComponent<Obstacle>(out Obstacle obstacle))
        {
            _losePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
