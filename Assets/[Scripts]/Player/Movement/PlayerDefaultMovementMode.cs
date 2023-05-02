using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Player.Movement
{
    public class PlayerDefaultMovementMode : PlayerMovementMode
    {
        #region Horizontal Movement Variables
        [Header("Horizontal Movement Settings")] 
        [SerializeField] private float speed = 5f;

        [SerializeField] private AnimationCurve accelerationCurve;
        [SerializeField] private AnimationCurve decelerationCurve;

        private int _horizontalMovementAxis = 0;
        private float _horizontalMovementTimer = 0.0f;
        [SerializeField] private float accelerationLength = 1f;
        [SerializeField] private float decelerationLength = 1f;
        private int _decelerationAxis = 0;
        private bool _shouldDecelerate;
        private float _decelerateTimer;

        private int moveDirection;

        [Header("Dash Settings")]
        [SerializeField] private float dashingForce = 24f;
        [SerializeField] private float dashingTime = 0.2f;
        [SerializeField] private float dashingCooldown = 1f;
        private bool _canDash = true;
        private bool _isDashing = false;
        private bool _isDashAvailable = true;

        #endregion

        #region Vertical Movement Variables
        [Header("Vertical movement settings")] 
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _springboardJumpForce;
        [SerializeField] private float jumpCooldown;
        [SerializeField] private float _springboardJumpCooldown;
        [SerializeField] private float _timeSinceSpringboardJump;
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
        #endregion

        #region Other Movement Variables
        [Header("Player comfort settings")] [SerializeField]
        private float _coyoteTimeDuration;

        [SerializeField] private float jumpBufferingDuration;
        private float _timeSinceLastJumpButtonPress;
        #endregion

        #region Hacking Related Variables
        [HideInInspector] public bool isGravityReversed = false;
        
        #endregion

        #region Assignments
        private PlayerControls _playerControls;
        private Rigidbody2D _rigidbody2D;
        private GroundChecker[] _groundCheckers;
        private PlayerMovementAnimatorController _playerMovementAnimatorController;
        private SpriteRenderer _spriteRenderer;
        private TrailRenderer _trailRenderer;
        #endregion

        protected override void Awake()
        {
            #region Assigning
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
            _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
            _initialPlayerGravityScale = _rigidbody2D.gravityScale;

            _groundCheckers = GetComponentsInChildren<GroundChecker>();

            _playerMovementAnimatorController = GetComponent<PlayerMovementAnimatorController>();

            _trailRenderer = GetComponent<TrailRenderer>();

            #endregion

            #region Input Actions
            _playerControls = new PlayerControls();
            _playerControls.Movement.Horizontal.performed += ctx =>
            {
                int move = (int)ctx.ReadValue<float>();
                if (move != 0)
                    moveDirection = move;

                ChangeMovementAxis(move);
            };
            _playerControls.Movement.Horizontal.canceled += _ => { ChangeMovementAxis(0); };

            _playerControls.Movement.Jump.performed += _ => _timeSinceLastJumpButtonPress = 0;

            _playerControls.Movement.Jump.canceled += _ => _shouldCutOffJump = true;

            _playerControls.Movement.Dash.performed += _ =>
            {
                if (_isDashAvailable)
                    StartCoroutine(Dash());
            };
            #endregion
        }

        private void Update()
        {
            if (_isDashing)
                return;
            GravityReverse();



            _horizontalMovementTimer += Time.deltaTime;
            _timeSinceLastJump += Time.deltaTime;
            _timeSinceBeingGrounded += Time.deltaTime;
            _timeSinceLastJumpButtonPress += Time.deltaTime;
            _timeSinceSpringboardJump += Time.deltaTime;
            if (_shouldDecelerate)
            {
                _decelerateTimer += Time.deltaTime;
            }


            _decelerateTimer = Mathf.Clamp(_decelerateTimer, 0, decelerationLength);
            if (_isJumping)
            {
                _currentJumpLength += Time.deltaTime;
            }


            foreach (GroundChecker checker in _groundCheckers)
            {
                if (checker.IsGrounded)
                {
                    OnBeingGrounded();
                    break;
                }

                OnBeingUngrounded();
            }

            if (_timeSinceLastJumpButtonPress < jumpBufferingDuration)
            {
                PerformJump();
            }

            if (_shouldCutOffJump && _currentJumpLength > minJumpLength && !_alreadyCutOffJump)
            {
                if (!isGravityReversed)
                {
                    if (Math.Sign(_rigidbody2D.velocity.y) == Math.Sign(transform.up.y))
                    {
                        _alreadyCutOffJump = true;
                        _rigidbody2D.velocity =
                            new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y * jumpCutOffFactor);
                    }
                }
                else if (Math.Sign(-_rigidbody2D.velocity.y) == Math.Sign(-transform.up.y))
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
        private void FixedUpdate()
        {
            if (_isDashing)
                return;
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


            #region Falling

            float verticalVelocity = _rigidbody2D.velocity.y;
            if (!isGravityReversed)
            {
                if (verticalVelocity<-maxFallingSpeed)
                    verticalVelocity = -maxFallingSpeed;
            }
            else if (isGravityReversed)
            {
                if (verticalVelocity > maxFallingSpeed)
                    verticalVelocity = maxFallingSpeed;
            }


            #endregion

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

        #region Grunding Related
        private void OnBeingGrounded()
        {
            _timeSinceBeingGrounded = 0f;
            _playerMovementAnimatorController.IsGrounded = true;
            _isDashAvailable = true;
        }

        private void OnBeingUngrounded()
        {
            _playerMovementAnimatorController.IsGrounded = false;

            if (!_isJumping)
            {
                _midAirMomentum = _horizontalMovementAxis;
                _playerMovementAnimatorController.IsGrounded = false;
            }
        }
        #endregion

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

            _playerMovementAnimatorController.HorizontalMovementValue = _horizontalMovementAxis;
        }
        #region Abilities
        private void GravityReverse()
        {
            if (Physics2D.gravity == new Vector2(0, 9.81f))
            {
                isGravityReversed = true;
                transform.localScale = new Vector3(1, -1, 1);
            }
            else
            {
                isGravityReversed = false;
                transform.localScale = new Vector3(1, 1, 1);
            }
        }

        private IEnumerator Dash()
        {
            _canDash = false;
            _isDashAvailable = false;
            _isDashing = true;
            _rigidbody2D.gravityScale = 0f;
            _rigidbody2D.velocity = new Vector2(moveDirection * dashingForce, 0f);
            _trailRenderer.emitting = true;
            yield return new WaitForSeconds(dashingTime);
            _trailRenderer.emitting = false;
            _rigidbody2D.gravityScale = _initialPlayerGravityScale;
            _isDashing = false;
            yield return new WaitForSeconds(dashingCooldown);
            _canDash = true;

        }
        #endregion

        #region Jump Methods
        private void PerformJump()
        {
            if (CanJump())
            {
                _timeSinceLastJump = 0;
                _isJumping = true;
                _alreadyCutOffJump = false;
                _shouldCutOffJump = false;
                if (!isGravityReversed)
                    _rigidbody2D.AddForce(transform.up * _jumpForce);
                else
                    _rigidbody2D.AddForce(-transform.up * _jumpForce);
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
            return _timeSinceLastJump > jumpCooldown && _timeSinceBeingGrounded < _coyoteTimeDuration && !_isJumping && !_isDashing;
        }

        #endregion

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Springboard") && _timeSinceSpringboardJump > _springboardJumpCooldown)
            {
                _timeSinceSpringboardJump = 0f;
                _rigidbody2D.velocity = Vector2.zero;
                if (!isGravityReversed)
                    _rigidbody2D.AddForce(Vector2.up * _springboardJumpForce);
                else
                    _rigidbody2D.AddForce(-Vector2.up * _springboardJumpForce);
            }
        }

        #region On Enable - On Disable
        private void OnEnable()
        {
            _playerControls.Enable();
        }

        private void OnDisable()
        {
            _playerControls.Disable();
        }
        #endregion
    }
}