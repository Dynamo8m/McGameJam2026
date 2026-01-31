//using System.Numerics;
//using System.Threading.Tasks.Dataflow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;
    private Vector2 movementInput;
    private Rigidbody2D body;
    //[SerializeField] private float speed;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        //body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.linearVelocity.y);
        //if (Input.GetKey(KeyCode.Space))
        {
        //    body.linearVelocity = new Vector2(body.linearVelocity.x, speed);
        }

        transform.Translate(new Vector3(movementInput.x, 0, movementInput.y) * speed * Time.deltaTime);


    }

    public void OnMove(InputAction.CallbackContext context) => movementInput = context.ReadValue<UnityEngine.Vector2>();
}
