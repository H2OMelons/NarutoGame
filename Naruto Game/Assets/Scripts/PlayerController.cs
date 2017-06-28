using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int playerNumber = 1;        // Used to identify the players
    public float speed = 1f;           // How fast the characters move left and right
    public LayerMask blockingLayer;     // Layer on which collision will be checked

    private string horizontalAxisName;  // Name of input axis to move character left and right
    private string verticalAxisName;    // Name of input axis for character jump
    private string attack1Name;         // Name of input for character attack 1
    private string attack2Name;         // Name of input for character attack 2
    private string attack3Name;         // Name of input for character attack 3
    private string weaponName;         // Name of input for character weapon attack
    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private float movementInputValue;
    private float jumpInputValue;
    private float attack1InputValue;
    private float attack2InputValue;
    private float attack3InputValue;
    private float weaponInputValue;

    private bool facingRight;
    private bool isRunning;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        // When player is playing make sure that it can move
        rb.isKinematic = false;
    }

    private void OnDisable()
    {
        // Make sure that when player is not playing, player can't move
        rb.isKinematic = true;
    }

    // Use this for initialization
    void Start ()
    {
        // Set the name of the inputs
        horizontalAxisName = "Horizontal" + playerNumber;
        verticalAxisName = "Vertical" + playerNumber;
        attack1Name = "Attack1_" + playerNumber;
        attack2Name = "Attack2_" + playerNumber;
        attack3Name = "Attack3_" + playerNumber;
        weaponName = "Weapon" + playerNumber;

        if (playerNumber == 1)
        {
            facingRight = true;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        movementInputValue = Input.GetAxisRaw(horizontalAxisName);
        jumpInputValue = Input.GetAxisRaw(verticalAxisName);
        attack1InputValue = Input.GetAxisRaw(attack1Name);
        attack2InputValue = Input.GetAxisRaw(attack2Name);
        attack3InputValue = Input.GetAxisRaw(attack3Name);
        weaponInputValue = Input.GetAxisRaw(weaponName);
	}

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        if (movementInputValue != 0)
        {
            if (movementInputValue > 0)
            {
                if (!facingRight)
                {
                    facingRight = true;
                    transform.Rotate(0, 180, 0);
                }
            }
            else
            {
                if (facingRight)
                {
                    facingRight = false;
                    transform.Rotate(0, -180, 0);
                }
            }
            transform.Translate(Vector3.right * speed * Time.deltaTime);

            if (!isRunning)
            {
                isRunning = true;
                animator.SetTrigger("Run");
            }
        }
        else 
        {
            if (isRunning)
            {
                isRunning = false;
                animator.SetTrigger("Idle");
            }
        }
        if (jumpInputValue > 0)
        {

        }
        if (attack1InputValue > 0)
        {

        }
        if (attack2InputValue > 0)
        {

        }
        if (attack3InputValue > 0)
        {

        }
        if (weaponInputValue > 0)
        {

        }
    }
}
