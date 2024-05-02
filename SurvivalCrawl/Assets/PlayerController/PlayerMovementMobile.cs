using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementMobile : MonoBehaviour
{
    Vector2 movementInput;
    public float speed = 10;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

   
    void Update()
    {
        rb.velocity = movementInput * speed;
    }
}
