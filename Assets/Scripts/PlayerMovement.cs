using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private float _movementAxis = 0;
    private PlayerControls _controls;

    private void Awake()
    {
        _controls = new PlayerControls();
    }

    private void Start()
    {
        _controls.Movement.Horizontal.performed += ctx => { _movementAxis = ctx.ReadValue<float>(); };
        _controls.Movement.Horizontal.canceled += _ => { _movementAxis = 0; };
    }

    private void FixedUpdate()
    {
        transform.position += Vector3.right * (_movementAxis * speed * Time.deltaTime);
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }
}