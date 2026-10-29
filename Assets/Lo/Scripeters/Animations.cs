using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class Animations : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private Vector2 _input;
    [SerializeField] private float interactRange = 0.3f;
    [SerializeField] private LayerMask pushableLayer;

    private ObstaclePullPushLo _ObstaclePullPushLo;
    private PlayerControllerLo _PlayerControllerLo;

    private CharacterController _characterController;

    void Start()
    {
        _ObstaclePullPushLo = GetComponent<ObstaclePullPushLo>();
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
        if (_ObstaclePullPushLo.isGrabbing)
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
            if (!_animator.GetBool("isWalking"))
            {
                _animator.SetBool("isCrouching", true);
            }
            else if (_animator.GetBool("isWalking"))
            {
                _animator.SetBool("isCrouchRunning", true);
            }
        }
        if (context.canceled)
        {
            _animator.SetBool("isCrouchRunning", false);
            _animator.SetBool("isCrouching", false);
        }
    }

    public void Walk(InputAction.CallbackContext context)
    {
        if (_ObstaclePullPushLo.isGrabbing) return;

        _input = context.ReadValue<Vector2>();
        if (_input != Vector2.zero && !_ObstaclePullPushLo.isGrabbing)
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

        if(_ObstaclePullPushLo.isGrabbing)
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
