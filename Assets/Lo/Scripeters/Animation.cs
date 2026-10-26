using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class Animation : MonoBehaviour
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

        if (Input.GetKeyUp(KeyCode.E)) // reseta as animações 
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

    public void Walk(InputAction.CallbackContext context) // animação de ANDAR (isWalking)
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
        // else
        // {
        //     _animator.SetBool("isJumping", false);
        // }
    }

    public void PullPush() //(InputAction.CallbackContext context) // animação de PUXAR e EMPURRAR
    {
        bool _touchingObject = Physics.CheckSphere(transform.position, interactRange, pushableLayer); // true se colide com empurraveis

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
