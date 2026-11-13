using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationsMain : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private Vector2 _input;
    [SerializeField] private float interactRange = 0.3f;
    [SerializeField] private LayerMask pushableLayer;

    private ObstaclePullPushMain _ObstaclePullPushMain;
    private PlayerController _PlayerController;
    private CharacterController _characterController;

    void Start()
    {
        _ObstaclePullPushMain = GetComponent<ObstaclePullPushMain>();
        _characterController = GetComponent<CharacterController>(); 
    }
    void Update()
    {
        PullPush();

        if (Input.GetKeyUp(KeyCode.E))
        {
            _animator.SetBool("isPulling", false);
            _animator.SetBool("isPushing", false);
        }
        if (_input == Vector2.zero)
        {
            _animator.SetBool("isWalking", false);
        }
        if (_characterController.isGrounded)
        {
            _animator.SetBool("isJumping", false);
        }
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _animator.SetBool("isCrouching", true);
        }
        if (context.canceled)
        {
            _animator.SetBool("isCrouching", false);
        }
    }

    public void Walk(InputAction.CallbackContext context)
    {
        if (_ObstaclePullPushMain.isGrabbing) return;

        _input = context.ReadValue<Vector2>();
        if (_input != Vector2.zero)
        {
                _animator.SetBool("isWalking", true);
        }
        else
        {
            _animator.SetBool("isWalking", false);
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(_characterController.isGrounded)
        {
            _animator.SetBool("isJumping", true);
        }
    }

    public void PullPush()
    {
        bool _touchingObject = Physics.CheckSphere(transform.position, interactRange, pushableLayer);

        if(_ObstaclePullPushMain.isGrabbing)
        {
            if(_touchingObject)
            {
                _animator.SetBool("isPushing", true);
                _animator.SetBool("isPulling", false);
            }
            if(!_touchingObject)
            {
                _animator.SetBool("isPulling", true);
                _animator.SetBool("isPushing", false);
            }
        }
        else
        {
            _animator.SetBool("isPushing", false);
            _animator.SetBool("isPulling", false);
        }
    }
}