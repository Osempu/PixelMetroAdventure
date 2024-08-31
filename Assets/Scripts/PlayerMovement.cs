using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    void Start()
    {
        _myRigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        characterXVelocity = _myRigidBody.velocity.x;
        characterYVelocity = _myRigidBody.velocity.y;

        if (_directionInput.x < 0)
            _spriteRenderer.flipX = true;

        if (_directionInput.x > 0)
            _spriteRenderer.flipX = false;

        if (_jumpValue > 0f)
            _animator.SetBool("isJumping", true);
        else
            _animator.SetBool("isJumping", false);

        Vector2 playerVelocity = new Vector2(_directionInput.x, _jumpValue is 0 ? _myRigidBody.velocity.y : _myRigidBody.velocity.y + jumpVelocity);

        _myRigidBody.velocity = playerVelocity * runningVelocity;


        if (_myRigidBody.velocity.x != 0f)
            _animator.SetBool("isRunning", true);
        else
            _animator.SetBool("isRunning", false);

        _jumpValue = 0f;
    }

    #region Public variables
    public float runningVelocity = 1f;
    public float jumpVelocity = 3f;
    #endregion

    #region Probe variables
    public float characterXVelocity;
    public float characterYVelocity;
    #endregion

    #region Gameobject components
    private Rigidbody2D _myRigidBody;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    #endregion

    #region Private variables
    private float _newXVelocity;
    private float _newYVelocity;
    private Vector2 _directionInput;
    private float _jumpValue;
    #endregion

    #region Flags
    private bool _isInMidAir;
    private bool _hasJumped;
    #endregion

    #region Unity events
    private void OnMove(InputValue value)
    {
        _directionInput = value.Get<Vector2>();
        if (_directionInput.x != 0f)
        {
            if (_directionInput.x > 0f)
                _spriteRenderer.flipX = false;
            else
                _spriteRenderer.flipX = false;

            _animator.SetBool("isRunning", true);
            _newXVelocity = _directionInput.x * runningVelocity;
        }

    }
    private void OnJump(InputValue value)
    {
        _jumpValue = value.Get<float>();
        if (!_isInMidAir)
        {
            if (value.isPressed)
            {
                _animator.SetBool("isJumping",true);

                _newYVelocity = _hasJumped is false ? jumpVelocity : _myRigidBody.velocity.y;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D otherCollider = collision.collider;
        if (otherCollider.CompareTag("Land"))
        {

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Collider2D otherCollider = collision.collider;
        if (otherCollider.CompareTag("Land"))
        {

        }
    }
    #endregion

    #region Function
    #endregion

    #region Methods
    private void PrintRelevantDataOnConsole()
    {
        Debug.Log(_directionInput);
        Debug.Log(_jumpValue);

    }
    #endregion

}
