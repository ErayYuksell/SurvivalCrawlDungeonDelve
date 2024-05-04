using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 movementInput;
    [Header("Movemont")]
    [SerializeField] float speed = 10;
    Rigidbody2D rb;
    InputSystem inputSystem;

    [Header("Animation")]
    [SerializeField] AnimationClip Runclip;
    [SerializeField] AnimationClip AttackClip;
    Animator animator;


    private void Awake()
    {
        inputSystem = new InputSystem();
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }
    private void FixedUpdate()
    {
        rb.velocity = movementInput * speed;

        PlayerFlip();

        if (movementInput == Vector2.zero)
        {
            return;
        }
        animator.Play(Runclip.name);
    }

    private void OnEnable()
    {
        inputSystem.Enable();
        inputSystem.PlayerInput.Movement.performed += MovementPerformed;
        inputSystem.PlayerInput.Attack.performed += AttackState;
        inputSystem.PlayerInput.AdvancedAttack.performed += AdvanceAttackState;
    }

    private void OnDisable()
    {
        inputSystem.Disable();
        inputSystem.PlayerInput.Movement.canceled -= MovemontCanceled;
        inputSystem.PlayerInput.Attack.canceled -= AttackState;
        inputSystem.PlayerInput.AdvancedAttack.performed -= AdvanceAttackState;
    }
    private void MovemontCanceled(InputAction.CallbackContext context) // value degeri donduruyorsam ayri fonksiyonlar kullanmayi tercih ettim
    {
        movementInput = Vector2.zero;
    }

    public void MovementPerformed(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void AttackState(InputAction.CallbackContext context) // button degeri dondurdugu icin tek fonksiyonda hallettim
    {
        if (context.performed)
        {
            animator.Play(AttackClip.name);
            Debug.Log("AttackPerformed");
        }
        if (context.canceled)
        {
            animator.StopPlayback();
            Debug.Log("AttackCanceled");
        }
    }
    private void AdvanceAttackState(InputAction.CallbackContext context)
    {
        Debug.Log("AdvanceAttack");
    }
    public void PlayerFlip()
    {
        bool playerSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon; // epsilon 0 a en yakin deger iste

        if (playerSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1); // sign icindeki deger 0 veya pozitif ise 1 dondurur negatif ise -1
        }
    }
}
