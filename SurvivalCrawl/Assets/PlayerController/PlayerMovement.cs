using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 movementInput;
    public float speed = 10;
    Rigidbody2D rb;
    InputSystem inputSystem;

    private void Awake()
    {
        inputSystem = new InputSystem();
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rb.velocity = movementInput * speed;
    }

    private void OnEnable()
    {
        inputSystem.Enable();
        inputSystem.PlayerInput.Movement.performed += MovementPerformed;
    }
    private void OnDisable()
    {
        inputSystem.Disable();
        inputSystem.PlayerInput.Movement.canceled -= MovemontCanceled;
    }

    private void MovemontCanceled(InputAction.CallbackContext context)
    {
        movementInput = Vector2.zero;
    }

    public void MovementPerformed(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
}
