using System;
using UnityEngine;

namespace Player.Movement
{
    public class PlayerDefaultMovementMode : PlayerMovementMode
    {
        [Header("Horizontal movement settings")] [SerializeField]
        private float speed = 5f;

        [SerializeField] private AnimationCurve horizontalMovementCurve;

        private int _horizontalMovementAxis = 0;
        private float _horizontalMovementTimer = 0.0f;
        [SerializeField] float accelerationLength = 1f;

        [Header("Vertical movement settings")] [SerializeField]
        private float jumpForce;

        [SerializeField] private float jumpCooldown;
        [SerializeField] private float maxJumpLength;
        [SerializeField] private float minJumpLength;
        [Range(0, 1)] [SerializeField] private float jumpCutOffFactor;
        private float appliedCutOffFactor = 1;
        private float _timeSinceBeingGrounded = 0;
        private float _timeSinceLastJump = 0;
        private float _currentJumpLength = 0;
        private bool _isJumping = false;
        private bool _shouldCutOffJump = false;
        private bool _alreadyCutOffJump = false;

        [Header("Player comfort settings")] [SerializeField]
        private float coyoteTimeDuration;

        [SerializeField] private float jumpBufferingDuration;
        private float _timeSinceLastJumpButtonPress;

        private PlayerControls _playerControls;
        private Rigidbody2D _rigidbody2D;
        private GroundChecker[] _groundCheckers;

        protected override void Awake()
        {
            _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
            _playerControls = new PlayerControls();
            _groundCheckers = GetComponentsInChildren<GroundChecker>();

            _playerControls.Movement.Horizontal.performed += ctx =>
            {
                ChangeMovementAxis((int) ctx.ReadValue<float>());
            };
            _playerControls.Movement.Horizontal.canceled += _ => { ChangeMovementAxis(0); };

            _playerControls.Movement.Jump.performed += _ => _timeSinceLastJumpButtonPress = 0;

            _playerControls.Movement.Jump.canceled += _ => _shouldCutOffJump = true;
        }

        private void Update()
        {
            _horizontalMovementTimer += Time.deltaTime;
            _timeSinceLastJump += Time.deltaTime;
            _timeSinceBeingGrounded += Time.deltaTime;
            _timeSinceLastJumpButtonPress += Time.deltaTime;
            if (_isJumping)
            {
                _currentJumpLength += Time.deltaTime;
            }


            foreach (GroundChecker checker in _groundCheckers)
            {
                if (checker.IsGrounded)
                {
                    _timeSinceBeingGrounded = 0f;
                    break;
                }
            }

            if (_timeSinceLastJumpButtonPress < jumpBufferingDuration)
            {
                PerformJump();
            }

            if (_shouldCutOffJump && _currentJumpLength > minJumpLength && !_alreadyCutOffJump)
            {
                if (Math.Sign(_rigidbody2D.velocity.y) == Math.Sign(transform.up.y))
                {
                    _alreadyCutOffJump = true;
                    _rigidbody2D.velocity =
                        new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y * jumpCutOffFactor);
                }
            }
        }

        private void FixedUpdate()
        {
            if (_currentJumpLength > maxJumpLength)
            {
                EndJump();
            }

            _rigidbody2D.velocity =
                new Vector2(_horizontalMovementAxis * speed *
                            horizontalMovementCurve.Evaluate(_horizontalMovementTimer / accelerationLength),
                    _rigidbody2D.velocity.y);
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
                
                _timeSinceLastJump = 0;
                _isJumping = true;
                _alreadyCutOffJump = false;
                _shouldCutOffJump = false;
                _rigidbody2D.AddForce(transform.up*jumpForce);
            }
        }

        private void EndJump()
        {
            if (_isJumping)
            {
                _isJumping = false;
                _currentJumpLength = 0;
            }
        }

        private bool CanJump()
        {
            return _timeSinceLastJump > jumpCooldown && _timeSinceBeingGrounded < coyoteTimeDuration && !_isJumping;
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