using UnityEngine;
using UnityEngine.InputSystem;
public class MovementMainchar : MonoBehaviour
{
    //Variables used to adjust movement.
    [SerializeField] int SpeedParameter = 1;
    [SerializeField] float JumpParameter = 50f;
    [SerializeField] int MaxNumberOfJumps = 3;

    //Public variables using in animation.
    public bool isRunning;
    public bool hasJumped;
    public bool isPropelledUpwards;
    public bool isFalling;
    public bool isWallGrabbing;

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
        UpdateAnimatorStateVariables();
        Run();
        Jump();
        ResetStateVariables();
        currentYVelocity = thisCharRigidBody.linearVelocityY;
    }
    void UpdateAnimatorStateVariables()
    {
        thisCharAnimator.SetBool("Is Running", isRunning);
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
            thisCharRigidBody.linearVelocity += new Vector2(0f, JumpParameter);
            hasJumped = false;
        }
    }
    void OnJump()
    {
        if (currentJumpsUsed < MaxNumberOfJumps)
        {
            hasJumped = true;
            isPropelledUpwards = true;
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
}
