using UnityEngine;

[RequireComponent(typeof(SwipeInput))]
[RequireComponent(typeof(PlayerJump))]
[RequireComponent(typeof(PlayerRollOver))]
[RequireComponent(typeof(RoadSwitcher))]
public class PlayerInput : MonoBehaviour
{
    private RoadSwitcher _roadSwitcher;
    private Animator _animator;
    private SwipeInput _swipes;
    private PlayerJump _player;
    private PlayerRollOver _playerRollOver;

    private int _index = 1;
    private int _shiftLeft = 0;
    private int _shiftRight = 2;

    private Coroutine _coroutine;

    private void Start()
    {
        _player = GetComponent<PlayerJump>();
        _playerRollOver = GetComponent<PlayerRollOver>();
        _roadSwitcher = GetComponent<RoadSwitcher>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _swipes = GetComponent<SwipeInput>();
        _swipes.Swiped += OnSwiped;
    }

    private void OnDisable()
    {
        _swipes.Swiped -= OnSwiped;
    }

    private void OnSwiped(Vector2 direction)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        if (direction == Vector2.right)
        {
            if (_index < _shiftRight)
                _index++;
        }

        if (direction == Vector2.left)
        {
            if (_index > _shiftLeft)
                _index--;
        }

        if (direction == Vector2.up)
        {
            _player.TryJump();
        }

        if (direction == Vector2.down)
        {
            StartCoroutine(_playerRollOver.RollOver());
        }

        _coroutine = StartCoroutine(_roadSwitcher.ChangeRoad(transform, _index));
    }
}
