using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int playerNumber = 1;        // Used to identify the players
    public float speed = 1f;            // How fast the characters move left and right
    public LayerMask blockingLayer;     // Layer on which collision will be checked
    public float jumpForce = 1f;
    public float attackCombo1MovementValue = .5f;
    public float attackCombo2MovementValue = .5f;
    public float attackCombo3MovementValue = .5f;

    private const float A2_COMBO_TIME = 10f;
    private const float A3_COMBO_TIME = 10f;

    private string attackCombo1TriggerName = "AttackCombo1";
    private string attackCombo2TriggerName = "AttackCombo2";
    private string attackCombo3TriggerName = "AttackCombo3";
    private string idleTriggerName = "Idle";
    private string jumpTriggerName = "Jump";
    private string runTriggerName = "Run";
    private string weaponTriggerName = "WeaponThrow";
    private string winningPoseTriggerName = "WinningPose";

    private string attackCombo1PS = "0";
    private string attackCombo2PS = "1";
    private string attackCombo3PS = "2";
    private string idlePS = "3";
    private string jumpPS = "4";
    private string runPS = "5";
    private string weaponThrowPS = "6";
    private string jumpAttackPS = "40";
    private string jumpMovePS = "45";
    private string jumpWeaponThrowPS = "46";

    private string playerState;

    private string horizontalAxisName;  // Name of input axis to move character left and right
    private string verticalAxisName;    // Name of input axis for character jump
    private string attack1Name;         // Name of input for character attack 1
    private string attack2Name;         // Name of input for character attack 2
    private string attack3Name;         // Name of input for character attack 3
    private string weaponName;          // Name of input for character weapon attack

    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D boxCollider;

    private float movementInputValue;
    private float attackCombo1TimeLeft = 0f;
    private float attackCombo2TimeLeft = 0f;

    private bool jumpInputValue;
    private bool attack1InputValue;
    private bool attack2InputValue;
    private bool attack3InputValue;
    private bool weaponInputValue;
    private bool facingRight;
    private bool isRunning;
    private bool isJumping;
    private bool isIdle = true;
    private bool isAttackCombo1;
    private bool isAttackCombo2;
    private bool isAttackCombo3;
    private bool queueAttackCombo2;
    private bool queueAttackCombo3;

    private bool inputLocked = false;
    private bool gameStopped = true;

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
        playerState = idlePS;
        if (playerNumber == 1)
        {
            facingRight = true;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!gameStopped) {
            movementInputValue = Input.GetAxisRaw(horizontalAxisName);
            jumpInputValue = Input.GetAxisRaw(verticalAxisName) > 0;
            attack1InputValue = Input.GetAxisRaw(attack1Name) > 0;
            attack2InputValue = Input.GetAxisRaw(attack2Name) > 0;
            attack3InputValue = Input.GetAxisRaw(attack3Name) > 0;
            weaponInputValue = Input.GetAxisRaw(weaponName) > 0;
            if (attackCombo1TimeLeft > 0)
            {
                attackCombo1TimeLeft -= Time.deltaTime;
            }
            if (attackCombo2TimeLeft > 0)
            {
                attackCombo2TimeLeft -= Time.deltaTime;
            }
        }
        CalculatePlayerState();
	}

    private void CalculatePlayerState()
    {
        // If player hit the jump key
        if (gameStopped)
        {
            playerState = idlePS;
        }
        else if (jumpInputValue)
        {
            if (isJumping)
            {
                // If player is jumping and moving, set player state to be jump move
                if (movementInputValue != 0)
                {
                    playerState = jumpMovePS;
                }
                // Otherwise set player state to be jump
                else
                {
                    playerState = jumpPS;
                }
            }
            else
            {
                playerState = jumpPS;
            }
        }
        // If player hit attack combo 1 key then set player state to be attack combo 1
        else if (attack1InputValue)
        {
            if (!isJumping)
            {
                playerState = attackCombo1PS;
                
            }
        }
        // If player hit attack combo 2 key then set player state to be attack combo 2
        // if the previous game state was attack combo 1
        else if (attack2InputValue)
        {
            if (attackCombo1TimeLeft > 0)
            {
                queueAttackCombo2 = true;
            }
        }
        // If player hit attack combo 3 key then set pllayer state to be attack combo 3
        // if the previous game state was attack combo 2
        else if (attack3InputValue)
        {
            if (attackCombo2TimeLeft > 0)
            {
                queueAttackCombo3 = true;
            }
        }
        // If player hit the left/right key
        else if (movementInputValue != 0)
        {
            if (isJumping)
            {
                playerState = jumpMovePS;
            }
            else
            {
                playerState = runPS;
            }
        }
        // Default player state of idle
        else
        {
            playerState = idlePS;
        }
    }

    private void FixedUpdate()
    {
        if (!inputLocked)
        {
            Movement();
        }
    }

    private void Movement()
    {
        // Check if the player is using attack combo part 1
        if (playerState == attackCombo1PS)
        {
            if (!isAttackCombo1)
            {
                transform.Translate(Vector3.right * attackCombo1MovementValue * Time.deltaTime);
                isAttackCombo1 = true;
                isIdle = false;
                inputLocked = true;
                attackCombo1TimeLeft = A2_COMBO_TIME;
                animator.SetTrigger(attackCombo1TriggerName);
            }
        }
        // Check if the player is jumping and moving
        else if (playerState == jumpMovePS)
        {
            Run();
        }
        // Check if the player is jumping only
        else if (playerState == jumpPS)
        {
            if (!isJumping)
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                isJumping = true;
                animator.SetTrigger(jumpTriggerName);
            }
        }
        // Check if the player is running
        else if (playerState == runPS)
        {
            Run();
        }
        // If none of the above then the character is idle
        else
        {
            isRunning = false;
            isAttackCombo1 = false;
            isAttackCombo2 = false;
            isAttackCombo3 = false;
            queueAttackCombo2 = false;
            queueAttackCombo3 = false;
            if (!isIdle && !isJumping)
            {
                isIdle = true;
                animator.SetTrigger(idleTriggerName);
            }
        }
    }

    private void Run()
    {
        // Update the rotation to match the direction of player input
        UpdateRotation();

        // Move the character in the desired direction
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

        // If character just entered the running state then set the 
        // appropriate flags and set the running animation
        if (playerState == runPS && !isRunning)
        {
            isRunning = true;
            isIdle = false;
            isJumping = false;
            animator.SetTrigger(runTriggerName);
        }
    }

    private void UpdateRotation()
    {
        if (movementInputValue > 0 && !facingRight)
        {
            facingRight = true;
            transform.Rotate(0, 180, 0);

        }
        else if (movementInputValue < 0 && facingRight)
        {
            facingRight = false;
            transform.Rotate(0, -180, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            if (isJumping)
            {
                isJumping = false;
                isIdle = true;
                animator.SetTrigger(idleTriggerName);
            } 
        }
    }

    public void AttackCombo1Finish()
    {
        inputLocked = false;
        if (queueAttackCombo2)
        {
            playerState = attackCombo2PS;
            if (!isAttackCombo2)
            {
                transform.Translate(Vector3.right * attackCombo2MovementValue * Time.deltaTime);
                isAttackCombo2 = true;
                isIdle = false;
                inputLocked = true;
                attackCombo2TimeLeft = A3_COMBO_TIME;
                animator.SetTrigger(attackCombo2TriggerName);
            }
        }
        else
        {
            isAttackCombo1 = false;
            queueAttackCombo3 = false;
        }
    }

    public void AttackCombo2Finish()
    {
        inputLocked = false;
        queueAttackCombo2 = false;
        if (queueAttackCombo3)
        {
            playerState = attackCombo3PS;
            if (!isAttackCombo3)
            {
                transform.Translate(Vector3.right * attackCombo3MovementValue * Time.deltaTime);
                isAttackCombo3 = true;
                isIdle = false;
                inputLocked = true;
                animator.SetTrigger(attackCombo3TriggerName);
            }

        }
        else
        {
            isAttackCombo1 = false;
            isAttackCombo2 = false;
            playerState = idlePS;
        }
    }

    public void AttackCombo3Finish()
    {
        isAttackCombo1 = false;
        isAttackCombo2 = false;
        isAttackCombo3 = false;
        inputLocked = false;
        queueAttackCombo3 = false;
        playerState = idlePS;
    }

    public void StartGame()
    {
        gameStopped = false;
    }

    public void Reset()
    {
        jumpInputValue = false;
        attack1InputValue = false;
        attack2InputValue = false;
        attack3InputValue = false;
        weaponInputValue = false;
        facingRight = false;
        isRunning = false;
        isJumping = false;
        isIdle = true;
        isAttackCombo1 = false;
        isAttackCombo2 = false;
        isAttackCombo3 = false;
        queueAttackCombo2 = false;
        queueAttackCombo3 = false;
        gameStopped = true;
    }
}
