using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    InputSystem input = null;
    Vector2 movementInput = Vector2.zero;
    Rigidbody2D rb;
    [SerializeField] float speed = 5;

    private void Awake()
    {
        input = new InputSystem();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        input.Enable();
        input.PlayerInput.Movement.performed += OnMovementPerformed;
        input.PlayerInput.Movement.canceled += OnMovementCancelled;
    }
    private void OnDisable()
    {
        input.Disable();
        input.PlayerInput.Movement.performed -= OnMovementPerformed;
        input.PlayerInput.Movement.canceled -= OnMovementCancelled;
    }
    private void FixedUpdate()
    {
        rb.velocity = movementInput * speed; // her cihaz icin fixlemek icin magnitude normalized Time.fixedDeltaTime bak ????
    }
    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
    private void OnMovementCancelled(InputAction.CallbackContext context)
    {
        movementInput = Vector2.zero;
    }
    //public InputActionReference movement, attack;
    //Vector2 movementInput;
    //public float speed = 5;

    //Rigidbody2D rb;
    //void Start()
    //{
    //    rb = GetComponent<Rigidbody2D>();
    //}

    //private void FixedUpdate()
    //{
    //    Movement();
    //}

    //public void Movement()
    //{
    //    movementInput = movement.action.ReadValue<Vector2>();
    //    rb.velocity = /*new Vector2(movementInput.x * speed, movementInput.y * speed);*/ movementInput.normalized * speed * Time.fixedDeltaTime;
    //}

    //private void OnEnable() // default unity fonksiyonu script aktif oldugunda direkt olarak calisir 
    //{
    //    attack.action.performed += PerformedAttack;
    //}

    //private void OnDisable()
    //{
    //    attack.action.performed -= PerformedAttack;
    //}
    //private void PerformedAttack(InputAction.CallbackContext context)
    //{
    //    // attack icin gerekli kodlar
    //}
}
