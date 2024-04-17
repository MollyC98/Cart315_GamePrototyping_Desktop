using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;

    [SerializeField] ParticleSystem hitEffect;

    Vector2 moveInput;

    Vector3 initialPosition;

    SpriteRenderer spriteRenderer;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;

    BoxCollider2D myFeetCollider;
    float gravityScaleAtStart;

    bool isAlive = true;

    // Start is called before the first frame update

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAlive){return;}
        Walk();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void OnMove(InputValue value)
    {
        if(!isAlive){return;}
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {
        if(!isAlive){return;}
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if(value.isPressed)
        {
            myRigidbody.velocity += new Vector2 (0f, jumpSpeed);

            bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
            myAnimator.SetBool("isJumping", playerHasHorizontalSpeed);

        }
    }

    void Walk()
    {
        if(!isAlive){return;}
        Vector2 playerVelocity = new Vector2 (moveInput.x * walkSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isWalking", playerHasHorizontalSpeed);

    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
        transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }

    void ClimbLadder()
    {
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myRigidbody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("isClimbing", false);

            return;
        }

        Vector2 climbVelocity = new Vector2 (myRigidbody.velocity.x, moveInput.y * climbSpeed);
        myRigidbody.velocity = climbVelocity;
        myRigidbody.gravityScale = 0f;


        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
    }
    

    void Die(){
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("enemy")))
        {
            isAlive = false;

            FindObjectOfType<GameSession>().ProcessPlayerDeath();
            StartCoroutine(Respawn(1f));
            
        }
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Laser Machine on"))) {

             isAlive = false;
            playHitEffect();
            StartCoroutine(destroyAfterHit());
            Debug.Log("here collide");

        }

  IEnumerator destroyAfterHit()
    {
      
       yield return new WaitForSeconds(.25f);
        FindObjectOfType<GameSession>().ProcessPlayerDeath();
        StartCoroutine(Respawn(0));
    }

         IEnumerator Respawn(float duration){
            spriteRenderer.enabled = false;
             myRigidbody.velocity = Vector2.zero;

             yield return new WaitForSeconds(duration);

             transform.position = initialPosition;

             spriteRenderer.enabled = true;

             isAlive = true;
         }
    }

    void playHitEffect (){
        if(hitEffect != null){
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            //Destroy(instance.gameObject, instance.main.duration+instance.main.startLifetime.constantMax);
        }
    }
}
