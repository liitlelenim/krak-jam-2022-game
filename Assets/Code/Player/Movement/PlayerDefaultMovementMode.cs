using System;
using UnityEngine;

namespace Player.Movement
{
    public class PlayerDefaultMovementMode : PlayerMovementMode
    {
        [Header("Horizontal movement settings")] 
        [SerializeField] private float speed = 5f;

        [SerializeField] private AnimationCurve horizontalMovementCurve;
        
        private int _horizontalMovementAxis = 0;
        private float _horizontalMovementTimer = 0.0f;
        private PlayerControls _playerControls;
        private Rigidbody2D _rigidbody2D;

        [Header("Vertical movement settings")] 
        [SerializeField] private float jumpForce;
        [SerializeField] private float jumpCooldown;
        [SerializeField] private float maxJumpLength;
        [SerializeField] private float minJumpLength;
        [SerializeField] private AnimationCurve jumpForceCurve;
        private float _timeSinceLastJump = 0;
        private float _currentJumpLength = 0;
        private bool _isJumping = false;
        private bool _shouldCutOffJump = false;
        [Header("Gravity settings")]
        [SerializeField] private float maxGravityMultiplier = 1.25f;
        [SerializeField] private float initialAfterJumpGravity = -4f;
        [SerializeField] private float gravityGrowthSpeed = 9f;
        private float _currentGravity = 0;
        protected override void Awake()
        {
            _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
            _playerControls = new PlayerControls();
            _playerControls.Movement.Horizontal.performed += ctx =>
            {
                ChangeMovementAxis((int) ctx.ReadValue<float>());
            };
            _playerControls.Movement.Horizontal.canceled += _ => { ChangeMovementAxis(0); };

            _playerControls.Movement.Jump.performed += _ => PerformJump();
            _playerControls.Movement.Jump.canceled += _ => _shouldCutOffJump = true;
        }

        private void Update()
        {
            _horizontalMovementTimer += Time.deltaTime;
            _timeSinceLastJump += Time.deltaTime;
            if (_isJumping)
            {
                _currentJumpLength += Time.deltaTime;
            }
            else
            {
                _currentGravity -= Time.deltaTime * gravityGrowthSpeed;
            }

            if (_shouldCutOffJump && _currentJumpLength > minJumpLength)
            {
                EndJump();
            }
        }

        private void FixedUpdate()
        {
            _currentGravity = Mathf.Clamp(_currentGravity, Physics2D.gravity.y * maxGravityMultiplier,0 );
            if (_currentJumpLength > maxJumpLength)
            {
                EndJump();
            }
            float verticalAxis = _isJumping ? CalculateCurrentJumpVelocity()  : _currentGravity;
            
            _rigidbody2D.velocity
                = Vector2.right * (speed * _horizontalMovementAxis *
                                   horizontalMovementCurve.Evaluate(_horizontalMovementTimer))
                + Vector2.up * verticalAxis;
        }
        private void ChangeMovementAxis(int axis)
        {
            _horizontalMovementAxis = axis;
            _horizontalMovementTimer = 0;
        }
        
        private void PerformJump()
        {
            if (CanJump())
            {
                _shouldCutOffJump = false;
                _timeSinceLastJump = 0;
                _isJumping = true;
            }
        }

        private void EndJump()
        {
            _currentGravity = -4;
            _currentJumpLength = 0;
            _isJumping = false;
            _shouldCutOffJump = false;
        }

        private bool CanJump()
        {
            return _timeSinceLastJump > jumpCooldown  && !_isJumping;
        }
        private float CalculateCurrentJumpVelocity()
        {
            return jumpForceCurve.Evaluate(_currentJumpLength/maxJumpLength)*jumpForce;
        }
        
        private void OnEnable()
        {
            _playerControls.Enable();
        }

        private void OnDisable()
        {
            _playerControls.Disable();
        }
    }
}