using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseOrbit : MonoBehaviour
{
    public Transform player;
    public float distance = 4.5f;
    public float MouseSensitivity = 0.2f;
    public float rotX;
    public float rotY;

    public float min = 0f;
    public float max = 75f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }
    void LateUpdate()
    {
        if (player != null)
        {
            Vector2 delta = Mouse.current.delta.ReadValue() * MouseSensitivity;
            rotX += delta.x;
            rotY = Math.Clamp(rotY + delta.y,min,max);

            Quaternion rotation = Quaternion.Euler(rotY, rotX, 0);
            transform.position = player.position - (rotation * Vector3.forward * distance);
            transform.rotation = rotation;
        }
    }
}
