using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private float _movementAxis = 0;

    [SerializeField] private float jumpStrength = 10f;

    [SerializeField] private float maxLedgeTolerance = 0.25f;
    private float _lastLedgeTimer = 0f;
    [SerializeField] private Vector2[] groundingCheckBox = new Vector2[2];

    [SerializeField] private float jumpCooldown = 0.2f;
    private float _lastJumpTimer = 0f;

    [SerializeField] private float jumpCutOffFactor = 0.7f;

    private PlayerControls _controls;
    private Rigidbody2D _rigidbody2D;
    private HackingMode _hackingMode;

    private void Awake()
    {
        _controls = new PlayerControls();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _hackingMode = GetComponent<HackingMode>();
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
        _lastLedgeTimer = IsGrounded() ? 0 : _lastLedgeTimer + Time.deltaTime;
        _lastJumpTimer += Time.deltaTime;
    }


    private void FixedUpdate()
    {
        if (!_hackingMode.playerIsHacking)
            transform.position += Vector3.right * (_movementAxis * speed * Time.deltaTime);
    }

    private void Jump()
    {
        if (_lastJumpTimer > jumpCooldown && _lastLedgeTimer < maxLedgeTolerance && !_hackingMode.playerIsHacking)
        {
            _lastJumpTimer = 0f;
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpStrength);
        }
    }

    private void CutOffJump()
    {
        if (_rigidbody2D.velocity.y > 0)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y * jumpCutOffFactor);
        }
    }

    private bool IsGrounded()
    {
        Collider2D[] overlapped =
            Physics2D.OverlapAreaAll((Vector2) transform.position + groundingCheckBox[0],
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