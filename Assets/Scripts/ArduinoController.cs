using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;


public class ArduinoController : MonoBehaviour
{
    private Rigidbody2D rb; 
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;
    private float amountToMove;
    [SerializeField] private LayerMask jumpableGround;
    private float dirX = 0f;
    //[SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float playerSpeed = 3f;
    [SerializeField] private float jumpForce = 15f;
    
    //public float moveSpeed
    //public float jumpForce;


    SerialPort sp = new SerialPort("COM5", 57600); 
   
    private enum MovementState { idle, running, jumping, falling };
    [SerializeField] private AudioSource jumpSoundEffect;

    //private Rigidbody2D myRigidbody;

    //public bool grounded;
    //public LayerMask whatIsGround;

    //private Collider2D myCollider;
    //private Animator myAnimator;

    // Start is called before the first frame update
    private void Start()
    {
        //myRigidbody = GetComponent<Rigidbody2D>();
        //myCollider = GetComponent<Collider2D>();
        //myAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        sp.Open();
        sp.ReadTimeout = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);
        //myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(playerSpeed, rb.velocity.y);
        amountToMove = playerSpeed * Time.deltaTime;

        if (sp.IsOpen)
        {
            try
            {
                MoveObject(sp.ReadByte());
                print(sp.ReadByte());
            }
            catch (System.Exception)
            {
            }
        }
        UpdateAnimationState();

        //anim.SetFloat("Speed", rb.velocity.x);
        // myAnimator.SetBool("Grounded", grounded);
    }

    void MoveObject(int Direction)
    {

        if (Direction == 14 && IsGrounded())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            //ScoreManager.instance.AddPoint();
            anim.SetBool("isJumping", true);
            //transform.Translate(Vector2.right * amountToMove, Space.World);
            transform.Translate(Vector2.right * Time.deltaTime * playerSpeed);

        }

        if (Direction == 0)
        {
            anim.SetBool("isJumping", false);
        }

    }
    
    private void UpdateAnimationState()
    {
        MovementState state; 
        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y>.1f) //upward force applied
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }
        anim.SetInteger("state", (int)state);
    }
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}