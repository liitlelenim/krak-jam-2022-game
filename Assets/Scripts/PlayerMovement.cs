using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private float _movementAxis = 0;

    [SerializeField] private float jumpStrength = 10f;

    [SerializeField] private float maxLedgeTolerance = 0.25f;
    private float _lastLedgeTimer = 0f;
    [SerializeField] private Vector2[] groundingCheckBox = new Vector2[4];

    [SerializeField] private float jumpCooldown = 0.2f;
    private float _lastJumpTimer = 0f;

    [SerializeField] private float jumpCutOffFactor = 0.7f;

    private PlayerControls _controls;
    private Rigidbody2D _rigidbody2D;
    private HackingMode _hackingMode;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private const string PlayerWalkingBool = "isWalking";
    private const string PlayerJumpingBool = "isJumping";

    private bool _gravityReversed = false;

    private void Awake()
    {
        _controls = new PlayerControls();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _hackingMode = GetComponent<HackingMode>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _controls.Movement.Horizontal.performed += ctx => _movementAxis = ctx.ReadValue<float>();
        _controls.Movement.Horizontal.canceled += _ => _movementAxis = 0;
        _controls.Movement.Jump.performed += _ => Jump();
        _controls.Movement.Jump.canceled += _ => CutOffJump();
    }

    private void Update()
    {
        _gravityReversed = Physics2D.gravity.y > 0;
        if (_gravityReversed)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 180));
        }
        else 
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

        _lastLedgeTimer = IsGrounded() ? 0 : _lastLedgeTimer + Time.deltaTime;
        _animator.SetBool(PlayerJumpingBool, _lastLedgeTimer != 0 );
        _animator.SetBool(PlayerWalkingBool, _movementAxis != 0);

        if (_movementAxis > 0 && !_hackingMode.playerIsHacking)
        {
            _spriteRenderer.flipX = true;
        }

        if (_movementAxis < 0 && !_hackingMode.playerIsHacking)
        {
            _spriteRenderer.flipX = false;
        }

        _lastJumpTimer += Time.deltaTime;
    }


    private void FixedUpdate()
    {
            transform.position += Vector3.right * (_movementAxis * speed * Time.deltaTime);
    }

    private void Jump()
    {
        if (_lastJumpTimer > jumpCooldown && _lastLedgeTimer < maxLedgeTolerance)
        {
            _lastJumpTimer = 0f;
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _gravityReversed ? -jumpStrength : jumpStrength);
        }
    }

    private void CutOffJump()
    {
        if (_rigidbody2D.velocity.y != 0)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y * jumpCutOffFactor);
        }
    }

    private bool IsGrounded()
    {
        Collider2D[] overlapped =
            Physics2D.OverlapAreaAll((Vector2) transform.position + (_gravityReversed
                    ? -groundingCheckBox[0]
                    : groundingCheckBox[0]),
                (Vector2) transform.position + (_gravityReversed
                    ? -groundingCheckBox[1]
                    : groundingCheckBox[1]));
        Debug.DrawLine((Vector2) transform.position + groundingCheckBox[0],
            (Vector2) transform.position + groundingCheckBox[1]);

        foreach (Collider2D collider in overlapped)
        {
            if (!collider.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
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