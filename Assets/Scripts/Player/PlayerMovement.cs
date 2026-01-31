using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        //Player movement
        float horizontalInput = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);


        // Flipping Character when moving
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }

        else if (horizontalInput > -0.01f)
        {
            transform.localScale = new Vector3(-1,1,1);
        }


        // Jumping
        if (Input.GetKey(KeyCode.Space))
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, speed);
        }



    }
}
