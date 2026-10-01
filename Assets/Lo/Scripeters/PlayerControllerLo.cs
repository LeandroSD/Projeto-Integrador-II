using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class PlayerControllerLo : MonoBehaviour
{
    private Vector2 _input;
    private CharacterController _characterController;
    private Vector3 _direction;

    [SerializeField] private float gravityMultiplier = 1; // multiplicador gravidade
    private float _gravity = -9.81f; // gravidade
    private float _velocity; // velocidade vertical

    [SerializeField] private float speed = 7; // velocidade movimento
    [SerializeField] private float jumpPower; // força do pulo
    private float _currentVelocity;

    [SerializeField] private float limiteFundo;
    [SerializeField] private float limiteBorda;

    [SerializeField] private float smoothTime = 0.05f; // tempo rotação do personagem

    [SerializeField] private LayerMask pushableLayer; // layer que objetos podem ser puxados (pro player parar rotação ao encostar neles)
    [SerializeField] private float interactRange = 0.25f; // range que detecta objetos com rigidbody na layer de cima ^^^^^^
    void Awake()
    {
        _characterController = GetComponent<CharacterController>(); 
    }
    void Update()
    {
        ApplyGravity();
        Rotation();

        _characterController.Move(_direction * speed * Time.deltaTime);

        bool _touchingObject = Physics.CheckSphere(transform.position, interactRange, pushableLayer); // true se colide com empurraveis
        if(Input.GetKey(KeyCode.E) && _touchingObject)
        {
            speed = 2.5f;
        }
        else
        {
            speed = 7f;
        }

        ChecarLimites(); // Manter a checagem de limitacao de posição apos a aplicação do movimento - NÃO MEXER
    }
//sistema movimentacao
    public void Move(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>(); //coloca o valor das teclas do sistema de input na variavel _input
        _direction = new Vector3(_input.x, 0.0f, _input.y);

        Vector3 movimentoFinal = (_direction * _velocity) + (_direction.y * Vector3.up);
        _characterController.Move(movimentoFinal * Time.deltaTime);
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
        bool _touchingObject = Physics.CheckSphere(transform.position, interactRange, pushableLayer); // true se colide com empurraveis
        if(Input.GetKey(KeyCode.E) && _touchingObject) return;
        
        if(!context.started) return; //nao roda o codigo durante o pulo
        if(!isGrounded()) return; //nao roda o codigo se o personagem esta no ar 

        _velocity += jumpPower; //velocidade vertical
    }
//limita o movimento do personagem no eixo X
    private void ChecarLimites()
    {
         if(transform.position.x > limiteFundo)
        {
            Debug.Log("Passou");
            
            transform.position = new Vector3(limiteFundo, transform.position.y, transform.position.z);
        }
        else if(transform.position.x < limiteBorda)
        {
            transform.position = new Vector3(limiteBorda, transform.position.y, transform.position.z);
        }
    }
    private bool isGrounded() => _characterController.isGrounded;
    
//rotação do personagem
    public void Rotation()
    {
        bool _touchingObject = Physics.CheckSphere(transform.position, interactRange, pushableLayer); // true se colide com empurraveis
        if(Input.GetKey(KeyCode.E))
        {
            interactRange = 0.5f;
        }
        else
        {
            interactRange = 0.25f;
        }

        if(_input.sqrMagnitude == 0 || _touchingObject) return;

        var targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
    }
}
