using System.Security.Cryptography;
using UnityEngine;

public class MovementMainchar : MonoBehaviour
{
    //Public variables declaration (can be changed directly at runtime to adjust behavior).
    public int HorizontalVelocity = 1;
    public int JumpHeight = 1;

    //Non-public variables declaration (for internal reference and property modification only).
    Rigidbody2D thisCharRigidBody;
    Vector2 rigidBodyNewVelocity;
    bool keyWasPressed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Initialization of variables.
        thisCharRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (keyWasPressed)
        {
            thisCharRigidBody.linearVelocity = rigidBodyNewVelocity;
            keyWasPressed = false;
        }
    }
    void OnJump()
    {
        keyWasPressed = true;
        rigidBodyNewVelocity = thisCharRigidBody.linearVelocity;
        rigidBodyNewVelocity.y = JumpHeight;
    }
    void OnMoveleft()
    {
        keyWasPressed = true;
        rigidBodyNewVelocity = thisCharRigidBody.linearVelocity;
        rigidBodyNewVelocity.x = HorizontalVelocity;
    }

    void OnMoveright()
    {
        keyWasPressed = true;
        rigidBodyNewVelocity = thisCharRigidBody.linearVelocity;
        rigidBodyNewVelocity.x = HorizontalVelocity * (-1);
    }
}
