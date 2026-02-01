using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerMovement : MonoBehaviour
{
   Animator animator;
    public float speed = 5f;
    private bool canJump;

    private bool nextHitIsB = false;
    private Vector3 originalScale;

    // ?? ADD THESE HERE
    public float jumpForce = 12f;
    private bool jumpPressed;

    private Rigidbody2D body;
    private Vector2 movementInput;

    void Start()
    {
        animator = GetComponent<Animator>();
    }



    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        body.interpolation = RigidbodyInterpolation2D.Interpolate;
        originalScale = transform.localScale;
        // helps jitter
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
            {
            jumpPressed = true;
            }
    }

    private void FixedUpdate()
    {
        // Horizontal movement only
        body.linearVelocity = new Vector2(movementInput.x * speed, body.linearVelocity.y);
        
        animator.SetFloat("Speed", Mathf.Abs(movementInput.x));
        animator.SetBool("Grounded", canJump);

        //Jump (apply once)
        if (jumpPressed && canJump)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
            canJump = false;
        }
        jumpPressed = false;

        // Flip sprite
        if (movementInput.x > 0.01f)
            transform.localScale = new Vector3(
                Mathf.Abs(originalScale.x),
                originalScale.y,
                originalScale.z
            );
        else if (movementInput.x < -0.01f)
            transform.localScale = new Vector3(
                -Mathf.Abs(originalScale.x),
                originalScale.y,
                originalScale.z
            );
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        if (nextHitIsB)
            animator.SetTrigger("Hit2");
        else
            animator.SetTrigger("Hit");

        nextHitIsB = !nextHitIsB; // flip for next time
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platfroms"))
        {
            canJump = true;
            Debug.Log("LEFT COLLISION");
        }
    }


}
