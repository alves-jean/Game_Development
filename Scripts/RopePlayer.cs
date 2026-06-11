using UnityEngine;
using UnityEngine.InputSystem;

public class RopePlayer : MonoBehaviour
{
    public float jumpForce = 6f;
    public float ropeJumpForceX = 7f;
    public float climbSpeed = 3f;
    public float normalGravity = 4f;
    public float sideOffset = 0.35f;
    public float grabCooldown = 0.15f;

    private Rigidbody2D rb;
    private Transform currentRope;

    private bool isHoldingRope;
    private float lastJumpTime;
    private int ropeSide = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = normalGravity;
    }

    void Update()
    {
        if (!isHoldingRope) return;

        // Trocar de lado na corda
        if (Keyboard.current.aKey.wasPressedThisFrame || Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            ropeSide = -1;
            MoveToSide();
        }

        if (Keyboard.current.dKey.wasPressedThisFrame || Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            ropeSide = 1;
            MoveToSide();
        }

        // Subir e descer na corda
        float vertical = 0;

        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
            vertical = 1;

        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
            vertical = -1;

        rb.linearVelocity = new Vector2(0, vertical * climbSpeed);

        // Pular para a próxima corda
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            JumpFromRope();
        }
    }

    void GrabRope(Transform rope)
    {
        if (Time.time < lastJumpTime + grabCooldown) return;

        currentRope = rope;
        isHoldingRope = true;

        rb.gravityScale = 0;
        rb.linearVelocity = Vector2.zero;

        ropeSide = transform.position.x < rope.position.x ? -1 : 1;

        MoveToSide();
    }

    void MoveToSide()
    {
        if (currentRope == null) return;

        transform.position = new Vector3(
            currentRope.position.x + sideOffset * ropeSide,
            transform.position.y,
            transform.position.z
        );
    }

    void JumpFromRope()
    {
        isHoldingRope = false;
        currentRope = null;

        lastJumpTime = Time.time;

        rb.gravityScale = normalGravity;

        rb.linearVelocity = new Vector2(
            ropeSide * ropeJumpForceX,
            jumpForce
        );
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Rope") && !isHoldingRope)
        {
            GrabRope(other.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Rope") && other.transform == currentRope)
        {
            isHoldingRope = false;
            currentRope = null;
            rb.gravityScale = normalGravity;
        }
    }
}