using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Rigidbody2D rigidbody2D;
    private float speed;
    InputAction moveAction;
    void Start()
    {
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        rigidbody2D.linearDamping = 4;
        moveAction = InputSystem.actions.FindAction("Move");
        speed = 20;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        rigidbody2D.AddForce(speed * Time.deltaTime * moveValue, ForceMode2D.Impulse);
        Debug.Log(rigidbody2D.position);

        /*
        Vector3 pos = transform.position;

        if (moveValue.x > 0.1)
        {
            pos.x += moveValue.x * speed * Time.deltaTime;
        } else if (moveValue.x < -0.1)
        {
            pos.x += moveValue.x * speed * Time.deltaTime;
            
        }

        transform.position = pos;*/
        
    }
}
