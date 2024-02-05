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
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float playerSpeed = 2.5f;
    [SerializeField] private float jumpForce = 14f;

    //public float moveSpeed;
    //private float amountToMove;
    //public float jumpForce;


    SerialPort sp = new SerialPort("COM5", 57600); 
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
        //amountToMove = moveSpeed * Time.deltaTime;
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

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

        anim.SetFloat("Speed", rb.velocity.x);

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
            transform.Translate(Vector2.right * Time.deltaTime * playerSpeed, Space.World);

        }

        if (Direction == 0)
        {
            anim.SetBool("isJumping", false);
        }
    }
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}