using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Player Info")]
    [SerializeField] public float moveSpeed = 5f; // �̵� �ӵ�
    [SerializeField] public float jumpForce = 10f; // ���� ��

    [SerializeField] public float coyoteTime = 0.2f;
    [SerializeField] public float jumpBufferTime = 0.2f;

    private float gravityScale = 3.5f;

    [Header("Ground Check")]
    [SerializeField] private bool isGrounded; // �ٴڿ� �ִ��� ����
    [SerializeField] public float groundCheckDistance;
    [SerializeField] public Transform groundCheck; // �ٴ� üũ ��ġ
    [SerializeField] public Vector2 groundCheckSize = new Vector2(1f, 0.1f); // �ٴ� üũ �ڽ� ũ��
    [SerializeField] public LayerMask groundLayer; // �ٴ� ���̾� ����ũ

    [Header("Component")]
    public Rigidbody2D rb { get; private set; }
    private PlayerAnimator animator;
    private PlayerSkill playerSkill;

    [Header("IsAtcitoning")]
    [SerializeField] public bool isPlatform = false;
    [SerializeField] private bool isJumping; // ���� ������ ����
    [SerializeField] private bool isFacingRight = false; // �÷��̾ �������� ���� �ִ��� ����

    private int facingDir;
    private int moveInput = 0;

    private float coyoteTimeCounter;
    private float jumpBufferCounter;

    public bool isDashing = false;
    public bool isAttack = false;

    // �߰��� ������
    [Header("Double Jump")]
    [SerializeField] private bool canDoubleJump = true; // ���� ���� ����� �Ѱ� ���� ����
    private bool doubleJumpAvailable = false; // ���� ���� ���� ����

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerSkill = GetComponent<PlayerSkill>();
        animator = GetComponent<PlayerAnimator>();

        gravityScale = rb.gravityScale;
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }

        Jump();

        GravitySetting();

        AnimationController();
    }

    private void GravitySetting()
    {
        if (playerSkill.isUmbrellaOpen && rb.velocity.y <= 0)
        {
            rb.gravityScale = playerSkill.umbrellaFallMultiplier;
        }
        else
        {
            rb.gravityScale = gravityScale;
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        // �¿� �̵�
        MoveInput();

        // ĳ���� ���� ����
        Flip();

        // �ٴ� üũ
        GroundCheck();
    }

    void MoveInput()
    {
        moveInput = 0;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveInput = -1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveInput = 1;
        }
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    private void Flip()
    {
        if (isFacingRight && moveInput < 0f || !isFacingRight && moveInput > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void Jump()
    {
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
            doubleJumpAvailable = true; // �ٴڿ� ������ ���� ���� ����
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            if(jumpBufferCounter > 0)
            {
                jumpBufferCounter -= Time.deltaTime;
            }
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping)
        {
            PerformJump();
        }
        else if (canDoubleJump && doubleJumpAvailable && !isGrounded && Input.GetButtonDown("Jump"))
        {
            PerformJump();
            doubleJumpAvailable = false; // ���� ���� ��� �Ŀ��� ���� ���� �Ұ�
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            coyoteTimeCounter = 0f;
        }
    }

    private void PerformJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        jumpBufferCounter = 0f;
        StartCoroutine(JumpCooldown());
    }

    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
    }

    public bool canUmbrella()
    {
        return !isDashing && !isAttack;
    }

    private void GroundCheck()
    {
        if (!isPlatform)
        {
            Collider2D collider = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f, groundLayer);
            isGrounded = collider != null;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void AnimationController()
    {
        if (isAttack)
        {
            animator.PlayAnimation("Attack");
        }
        else if (!isGrounded && rb.velocity.y < 0)
        {
            animator.PlayAnimation("Fall");
        }
        else if (!isGrounded && rb.velocity.y > 0)
        {
            animator.PlayAnimation("Jump");
        }
        else if (isGrounded && moveInput != 0)
        {
            animator.PlayAnimation("Move");
        }
        else if (isGrounded && moveInput == 0)
        {
            animator.PlayAnimation("Idle");
        }
    }


    void OnDrawGizmos()
    {
        // �ٴ� üũ ���� ǥ��
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
    }
}
