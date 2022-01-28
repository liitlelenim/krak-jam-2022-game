using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PathFollowAI : MonoBehaviour
{
    public bool ShouldFollow { get; set; } = true;
    public bool ShouldBeReversed { get; set; } = false;
    [SerializeField] private float speed = 3f;
    [SerializeField] Vector2[] path;

    private readonly Vector2 _gridOffset = new Vector2(-0.5f, -0.5f);
    private int _currentDestination = 0;
    private Vector2 _destinationDirection;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        ShouldBeReversed = true;
    }

    private void Start()
    {
        _destinationDirection = (path[0] + _gridOffset - (Vector2) transform.position).normalized;
        if (Vector2.Distance(transform.position, path[_currentDestination] + _gridOffset) <= 0.5f)
        {
            _currentDestination += ShouldBeReversed ? -1 : 1;
            if (_currentDestination > path.Length - 1)
            {
                _currentDestination = 0;
            }

            if (_currentDestination < 0)
            {
                _currentDestination = path.Length - 1;
            }
        }
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, path[_currentDestination] + _gridOffset) <= 0.5f)
        {
            _currentDestination += ShouldBeReversed ? -1 : 1;
            if (_currentDestination > path.Length - 1)
            {
                _currentDestination = 0;
            }

            if (_currentDestination < 0)
            {
                _currentDestination = path.Length - 1;
            }
        }

        _destinationDirection = (path[_currentDestination] + _gridOffset - (Vector2) transform.position).normalized;


        if (ShouldFollow)
        {
            transform.position += new Vector3(_destinationDirection.x, _destinationDirection.y, 0) *
                                  (Time.deltaTime * speed);
            if (_destinationDirection.x >= 0)
            {
                if (ShouldBeReversed)
                {
                    _spriteRenderer.flipX = true;
                }
                else
                {
                    _spriteRenderer.flipX = false;
                }
            }
            else
            {
                if (ShouldBeReversed)
                {
                    _spriteRenderer.flipX = false;
                }
                else
                {
                    _spriteRenderer.flipX = true;
                }
            }
        }
    }
}