using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class MovementMainchar : MonoBehaviour
{
    //Variables used to adjust movement.
    [SerializeField] int SpeedParameter = 1;
    [SerializeField] float JumpParameter = 50f;
    [SerializeField] int MaxNumberOfJumps = 3;

    //Variables used for state definition.
    bool isRunning;
    bool hasJumped;
    bool isAirRolling;
    bool isFalling;
    bool isWallGrabbing;
    bool isOnGround;

    //Variables for internal reference and property modification. Make them public to show in Unity Editor.
    Animator thisCharAnimator;
    Rigidbody2D thisCharRigidBody;
    Vector2 rigidBodyNewVelocity;
    Vector2 inputValue;
    SpriteRenderer thisCharSpriteRenderer;

    //Made public for runtime debugging.
    public int currentJumpsUsed = 0;

    public float currentYVelocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Initialization of variables.
        thisCharAnimator = GetComponent<Animator>();
        thisCharRigidBody = GetComponent<Rigidbody2D>();
        thisCharSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCertainVariables();
        UpdateAnimatorStateVariables();
        Run();
        Jump();
        ResetStateVariables();
    }
    void UpdateCertainVariables()
    {
        if (thisCharRigidBody.linearVelocityY < -1)
            isAirRolling = false;
    }
    void UpdateAnimatorStateVariables()
    {
        thisCharAnimator.SetBool("Is Running", isRunning);
        thisCharAnimator.SetBool("Has Jumped", hasJumped);
        thisCharAnimator.SetBool("Is Air Rolling", isAirRolling);
        thisCharAnimator.SetBool("Is On Ground", isOnGround);
        thisCharAnimator.SetFloat("Vertical Velocity", thisCharRigidBody.linearVelocityY);
    }
    void ResetStateVariables()
    {
    }
    void Run()
    {
        if (inputValue.x != 0)
            thisCharSpriteRenderer.flipX = inputValue.x > 0 ? false : true;
        rigidBodyNewVelocity = new Vector2(inputValue.x * SpeedParameter, thisCharRigidBody.linearVelocityY);
        thisCharRigidBody.linearVelocity = rigidBodyNewVelocity;
    }
    void Jump()
    {
        if (hasJumped)
        {
            rigidBodyNewVelocity = new Vector2(0f, JumpParameter);
            thisCharRigidBody.linearVelocity = rigidBodyNewVelocity;
            hasJumped = false;
            if (currentJumpsUsed > 1)
                isAirRolling = true;
        }

        currentYVelocity = thisCharRigidBody.linearVelocityY;


    }
    void OnJump()
    {
        if (currentJumpsUsed < MaxNumberOfJumps)
        {
            hasJumped = true;
            currentJumpsUsed++;
        }
    }

    void OnMove(InputValue value)
    {
        inputValue = value.Get<Vector2>();
        if (inputValue.x == 0)
            isRunning = false;
        else
            isRunning = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        string colliderTag = collision.gameObject.tag;
        if (colliderTag == "Land" || colliderTag == "Wall")
        {
            isOnGround = true;
            currentJumpsUsed = 0;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        string colliderTag = collision.gameObject.tag;
        if (colliderTag == "Land")
        {
            isOnGround = false;
        }
    }
}
