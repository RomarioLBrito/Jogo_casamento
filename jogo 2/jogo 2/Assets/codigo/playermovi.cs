using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class playermovi : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 20f;
    Rigidbody2D playerRigidbody;
    Vector2 movementInput;
    Animator playerAnimator;
    CapsuleCollider2D playerCapsuleCollider;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCapsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        SpriteFlip();
    }
    void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        Collider2D playerCollider = GetComponent<Collider2D>();

        if (!playerCollider.IsTouchingLayers(LayerMask.GetMask("plataformas")))
        {
            return;
        }

        if (value.isPressed)
        {
            playerRigidbody.velocity += new Vector2(0f, jumpForce);
        }
    }

    void Run()
    {
        Vector2 playerSpeed = new Vector2(movementInput.x * speed, playerRigidbody.velocity.y);
        playerRigidbody.velocity = playerSpeed;

        bool playerHasXSpeed = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;
        playerAnimator.SetBool("parado", playerHasXSpeed);

    }
    void SpriteFlip()
    {
        float scaleX = (Mathf.Sign(playerRigidbody.velocity.x)) * 5f;
        float scaleY = 5f;

        bool playerHasXSpeed = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasXSpeed)
        {
            transform.localScale = new Vector2(scaleX, scaleY);
        }
    }
}
