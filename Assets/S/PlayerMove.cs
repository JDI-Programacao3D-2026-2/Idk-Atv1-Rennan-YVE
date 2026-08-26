
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMove : MonoBehaviour
{
private float speed = 5;

public Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void FixedUpdate() 
    {
        Vector3 direction = Vector3.zero;

        if (Keyboard.current[Key.W].isPressed)
        {
            direction += transform.forward;
        }
        if (Keyboard.current[Key.S].isPressed)
        {
            direction -= transform.forward;
        }
        if (Keyboard.current[Key.A].isPressed)
        {
            direction -= transform.right;
        }
        if (Keyboard.current[Key.D].isPressed)
        {
            direction += transform.right;
        }
        if (Keyboard.current[Key.Space].isPressed)
        {
            transform.position += transform.forward * speed * Time.deltaTime;

        }

        direction = Vector3.ClampMagnitude(direction, 1f);
        rb.linearVelocity = new Vector3(direction.x * speed, rb.linearVelocity.y, direction.z * speed);
    
        
    }
}
