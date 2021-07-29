using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float jumpTime = 5f;
    [SerializeField] float jumpForce = 10f;

    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip deathSound;

    private float jumpTimerCounter;

    public static bool isAlive = true;
    private bool isJumping;

    private Rigidbody2D myRigidbody;
    private Animator myAnimator;
    private CapsuleCollider2D myBodyCollider;
    private BoxCollider2D myFeet;
    private AudioSource myAudioSource;

    void Start()
    {
        myAudioSource = this.GetComponent<AudioSource>();
        myRigidbody = this.GetComponent<Rigidbody2D>();
        myAnimator = this.GetComponent<Animator>();
        myBodyCollider = this.GetComponent<CapsuleCollider2D>();
        myFeet = this.GetComponent<BoxCollider2D>();
        isAlive = false;
        isJumping = false;
    }

    void Update()
    {
        if(!isAlive) { return; }
        Run();
        FlipSprite();
        Jump();
        Die();
    }

    private void Run() {
        float controlThrow = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed,myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running",playerHasHorizontalSpeed);
    }

    private void FlipSprite() {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if(playerHasHorizontalSpeed) {
            transform.localScale = new Vector2(-Mathf.Sign(myRigidbody.velocity.x)*.5f,.5f);
        }
    }

    private void Jump() {
        if(!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground","AngryBox"))) { 
            return;
        }
        if(Input.GetKeyDown(KeyCode.UpArrow)) {
            myAudioSource.PlayOneShot(jumpSound);
            Vector2 JumpVelocityToAdd = new Vector2(0f,jumpSpeed);
            myRigidbody.velocity += JumpVelocityToAdd;
        }
    }

    private void Die() {
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy","Hazards"))) {
            myAnimator.enabled = false;
            myRigidbody.velocity = Vector2.zero;
            if(isAlive) {
                myAudioSource.PlayOneShot(deathSound);
            }
            isAlive = false;
            EnemyOneMovement.stopMovement = true;
            myRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            FindObjectOfType<GameManagerScript>().ProcessPlayerDeath();
        }
    }
}
