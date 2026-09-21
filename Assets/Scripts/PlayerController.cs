using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    private Vector2 _input;
    private CharacterController _characterController;
    private Vector3 _direction;

    private float _gravity = -9.81f; // gravidade
    [SerializeField] private float gravityMultiplier = 1; // multiplicador gravidade
    private float _velocity; //velocidade vertical

    [SerializeField] private float speed; //velocidade movimento
    [SerializeField] private float jumpPower; //força do pulo

    void Awake()
    {
        _characterController = GetComponent<CharacterController>(); 
    }
    void Update()
    {
        ApplyGravity();
        _characterController.Move(_direction * speed * Time.deltaTime);
    }
//sistema movimentacao
    public void Move(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>(); //coloca o valor das teclas do sistema de input na variavel _input
        _direction = new Vector3(_input.x, 0.0f, _input.y);
    }
//aplicar gravidade
    private void ApplyGravity()
    {
        if (isGrounded() && _velocity < 0.0f)
        {
            _velocity = -1.0f;
        }
        else
        {
            _velocity += _gravity * gravityMultiplier * Time.deltaTime;
        }
        _direction.y = _velocity;
    }
//sistema de pulo
    public void Jump(InputAction.CallbackContext context)
    {
        if(!context.started) return; //nao roda o codigo durante o pulo
        if(!isGrounded()) return; //nao roda o codigo se o personagem esta no ar 

        _velocity += jumpPower; //velocidade vertical
    }
    private bool isGrounded() => _characterController.isGrounded;

}
