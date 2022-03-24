using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Player.Movement
{
    public class PlayerDefaultMovementMode : PlayerMovementMode
    {
        [Header("Horizontal movement settings")] [SerializeField]
        private float speed = 5f;

        [SerializeField] private AnimationCurve accelerationCurve;
        [SerializeField] private AnimationCurve decelerationCurve;


        private int _horizontalMovementAxis = 0;
        private float _horizontalMovementTimer = 0.0f;
        [SerializeField] float accelerationLength = 1f;
        [SerializeField] float decelerationLength = 1f;
        private int _decelerationAxis = 0;
        private bool _shouldDecelerate;
        private float _decelerateTimer;

        [Header("Vertical movement settings")] [SerializeField]
        private float jumpForce;

        [SerializeField] private float jumpCooldown;
        [SerializeField] private float maxJumpLength;
        [SerializeField] private float minJumpLength;
        [Range(0, 1)] [SerializeField] private float jumpCutOffFactor;
        [SerializeField] private float maxFallingSpeed = 11;
        [SerializeField] private float midAirDirectionChangeSpeed = 15f;
        private float _initialPlayerGravityScale;
        [SerializeField] private float jumpApogeeGravityScale = 4f;
        [SerializeField] private float jumpApogeeLength = 0.25f;
        private bool _shouldBeStayingAtApogee = false;
        private float _apogeeTimer;
        private float _timeSinceBeingGrounded = 0;
        private float _timeSinceLastJump = 0;
        private float _currentJumpLength = 0;
        private bool _isJumping = false;
        private bool _shouldCutOffJump = false;
        private bool _alreadyCutOffJump = false;
        private float _midAirMomentum = 0f;

        [Header("Player comfort settings")] [SerializeField]
        private float coyoteTimeDuration;

        [SerializeField] private float jumpBufferingDuration;
        private float _timeSinceLastJumpButtonPress;

        private PlayerControls _playerControls;
        private Rigidbody2D _rigidbody2D;
        private GroundChecker[] _groundCheckers;
    
        //Todo create proper player animation system
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        protected override void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            
            _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
            _playerControls = new PlayerControls();
            _groundCheckers = GetComponentsInChildren<GroundChecker>();
            _initialPlayerGravityScale = _rigidbody2D.gravityScale;
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
            _animator.SetBool("IsWalking",_horizontalMovementAxis != 0 && !_isJumping);
            if (_horizontalMovementAxis < 0)
            {
                _spriteRenderer.flipX = true;
            }
            else if(_horizontalMovementAxis > 0)
            {
                _spriteRenderer.flipX = false;
            }
            _horizontalMovementTimer += Time.deltaTime;
            _timeSinceLastJump += Time.deltaTime;
            _timeSinceBeingGrounded += Time.deltaTime;
            _timeSinceLastJumpButtonPress += Time.deltaTime;
            if (_shouldDecelerate)
            {
                _decelerateTimer += Time.deltaTime;
            }

            _decelerateTimer = Mathf.Clamp(_decelerateTimer, 0, decelerationLength);
            if (_isJumping)
            {
                _currentJumpLength += Time.deltaTime;
            }


            bool isGroundedDuringCurrentFrame = false;
            foreach (GroundChecker checker in _groundCheckers)
            {
                if (checker.IsGrounded)
                {
                    isGroundedDuringCurrentFrame = true;
                    OnBeingGrounded();
                    break;
                }
            }

            if (_timeSinceBeingGrounded == 0f && isGroundedDuringCurrentFrame == false)
            {
                OnBeingUngrounded();
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

                _shouldBeStayingAtApogee = false;
                _rigidbody2D.gravityScale = _initialPlayerGravityScale;
                _apogeeTimer = 0;
            }
        }

        private void OnBeingGrounded()
        {
            _timeSinceBeingGrounded = 0f;
        }

        private void OnBeingUngrounded()
        {
            if (!_isJumping)
            {
                _midAirMomentum = _horizontalMovementAxis;
            }
        }

        private void FixedUpdate()
        {
            if (_currentJumpLength > maxJumpLength)
            {
                EndJump();
            }

            if (!_shouldBeStayingAtApogee && _isJumping && _currentJumpLength > minJumpLength &&
                !_shouldBeStayingAtApogee && !_alreadyCutOffJump)
            {
                if (Mathf.Abs(_rigidbody2D.velocity.y) < 2f)
                {
                    _shouldBeStayingAtApogee = true;
                    _rigidbody2D.gravityScale = jumpApogeeGravityScale;
                }
            }

            if (_shouldBeStayingAtApogee)
            {
                _apogeeTimer += Time.deltaTime;
                if (_apogeeTimer > jumpApogeeLength)
                {
                    _rigidbody2D.gravityScale = _initialPlayerGravityScale;
                    _shouldBeStayingAtApogee = false;
                    _apogeeTimer = 0;
                }
            }

            float curveFactor;
            if (_shouldDecelerate)
            {
                curveFactor = decelerationCurve.Evaluate(_decelerateTimer / decelerationLength);
            }
            else
            {
                curveFactor = accelerationCurve.Evaluate(_horizontalMovementTimer / accelerationLength);
            }

            float verticalVelocity = _rigidbody2D.velocity.y;
            if (verticalVelocity * Math.Sign(-transform.up.y) > maxFallingSpeed)
            {
                verticalVelocity = maxFallingSpeed * Math.Sign(-transform.up.y);
            }

            float horizontalVelocity = 0;
            if (_timeSinceBeingGrounded == 0)
            {
                if (_shouldDecelerate)
                {
                    horizontalVelocity = _decelerationAxis * speed * curveFactor;
                }
                else
                {
                    horizontalVelocity = _horizontalMovementAxis * speed * curveFactor;
                }
            }
            else
            {
                if (_horizontalMovementAxis != 0)
                {
                    _midAirMomentum += Time.deltaTime * _horizontalMovementAxis * midAirDirectionChangeSpeed;
                }
                else
                {
                    if (Mathf.Abs(_midAirMomentum) <= 0.25f)
                    {
                        _midAirMomentum = 0;
                    }
                    else
                    {
                        _midAirMomentum -= Time.deltaTime * midAirDirectionChangeSpeed * Mathf.Sign(_midAirMomentum);
                    }
                }

                _midAirMomentum = Mathf.Clamp(_midAirMomentum, -1f, 1f);

                horizontalVelocity = speed * _midAirMomentum;
            }

            _rigidbody2D.velocity =
                new Vector2(horizontalVelocity, verticalVelocity);
        }

        private void ChangeMovementAxis(int axis)
        {
            _horizontalMovementTimer = 0;
            _decelerateTimer = 0;
            if (axis != 0)
            {
                _horizontalMovementAxis = axis;
                _shouldDecelerate = false;
            }
            else
            {
                _decelerationAxis = _horizontalMovementAxis;
                _horizontalMovementAxis = 0;
                _shouldDecelerate = true;
            }
        }

        private void PerformJump()
        {
            if (CanJump())
            {
                _timeSinceLastJump = 0;
                _isJumping = true;
                _alreadyCutOffJump = false;
                _shouldCutOffJump = false;
                _rigidbody2D.AddForce(transform.up * jumpForce);
                _midAirMomentum = _horizontalMovementAxis;
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