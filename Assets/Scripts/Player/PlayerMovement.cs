using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private bool canJump;

    // ?? ADD THESE HERE
    public float jumpForce = 12f;
    private bool jumpPressed;

    private Rigidbody2D body;
    private Vector2 movementInput;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        body.interpolation = RigidbodyInterpolation2D.Interpolate; // helps jitter
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
        
        //Jump (apply once)
        if (jumpPressed && canJump)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
            canJump = false;
        }
        jumpPressed = false;

        // Flip sprite
        if (movementInput.x > 0.01f)
            transform.localScale = Vector3.one;
        else if (movementInput.x < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platfroms"))
        {
            Debug.Log("COLLIDING WITH: " + collision.gameObject.name);
            canJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platfroms"))
        {
            canJump = false;
            Debug.Log("LEFT COLLISION");
        }
    }


}
