using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;
    private Rigidbody2D body;

    private Vector2 movementInput;
    //[SerializeField] private float speed;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        //Player movement
        //float horizontalInput = Input.GetAxis("Horizontal");
       // body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        transform.Translate(new Vector3(movementInput.x, 0, movementInput.y) * speed * Time.deltaTime);


        // Flipping Character when moving
        //if (movementInput.x > 0.01f)
        //{
        //    transform.localScale = Vector3.one;
        //}

        //else if (movementInput.x < -0.01f)
        //{
        //    transform.localScale = new Vector3(-1,1,1);
        //}


        //Jumping
        //if (Input.GetKey(KeyCode.Space))
        //{
        //   body.linearVelocity = new Vector2(body.linearVelocity.x, speed);
        //}

        
        
        

    }
    public void OnMove(InputAction.CallbackContext context) => movementInput = context.ReadValue<UnityEngine.Vector2>();

}
