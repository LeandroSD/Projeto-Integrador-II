using UnityEngine;
using UnityEngine.InputSystem;

public class Locomocao : MonoBehaviour
{
    //variavel que irá armazenar os inputs de movimentação
    [SerializeField] private Vector2 _inputMovimentacao;

    //Acesso aos componentes do objeto
    [Header("Componentes")]
    [SerializeField] private CharacterController _characterController;

    //Variavies que irão gerenciar os atributos de movimentação do objeto
    [Header("Atributos de movimentação")]
    [SerializeField] private float _velocidadeMovimentacao;  
    [SerializeField] private float _multiplicadorGravidade;
    [SerializeField] private float _forcaPulo; 
    private Vector3 _movimentoHorizontal = Vector3.zero;
    private Vector3 _movimentoVertical = Vector3.zero;

    //Variaveis responsaveis por gerenciar a gravidade e pulo do personagem
    [SerializeField]private bool _estaNoChao = true;
    [SerializeField]private bool _podePular = false;
    private float _gravidade = -9.81f;

    [Header("Configurações e acesso da camera")]
    [SerializeField] private Transform _cameraVirtual;
    [SerializeField] private float _velocidadeRotacao;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Mover();
        Pular();
    }

    //Métodos para realizar a leitura dos inputs
    public void GetMoverInput(InputAction.CallbackContext context)
    {
        _inputMovimentacao = context.ReadValue<Vector2>();
    }

    //Método responsavel por realizar a leitura do input de pulo
    public void GetPularInput(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            _podePular = true;
        }
        else
        {
            _podePular = false;
        }
    }

    
    //Método que irá aplicar o movimento no personagem com o character controller
    private void Mover()
    {
        //Chamando o método que aplicar gravidade no objeto
        CalcularGravidade();

        if(_movimentoHorizontal != Vector3.zero)
        {
            //Quarternions são estruturas matemáticas utilizadas  para representar rotações - Um quarternion possui uma estrutura de quatro números reais
            //Desse modo, variaveis X, Y, Z representam a rotação em angulos em cada um desses eixos, Por fim,
            //Temos o w que representa a quantidade de rotação relacionada ao angulo

            //O método LookRtation presente na classe Quartenion, cria uma rotação com uma direção especificada
            //Desse modo, passamos como parametro um vetor onde iremos considerar a direção para a frente
            //Por fim. indicamos em qual eixo essa rotação será aplicada
            Quaternion rotacaoFinal = Quaternion.LookRotation(_movimentoHorizontal, Vector3.up);

            //Zerando a rotação no eixo X e Y do objeto
            rotacaoFinal.x = 0;
            rotacaoFinal.z = 0;

            //Aplicando a rotação suave no objeto utilizando o método Slerp da classe Quaternion - Interpolação esférica (Aplica movimento mais natural)
            //Há também o método Lerp que cria uma interpolação Linear entre um ponto e outro
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacaoFinal, _velocidadeRotacao * Time.deltaTime);
        }

        //Definindo o movimento horizontal do personagem a partir dos inputs pressionados 
        //_movimentoHorizontal = new Vector3 (_inputMovimentacao.x, 0, _inputMovimentacao.y);

        //Definindo o movimento hotizontal do personagem a partir dos inputs pressionados e da direção da camera

        _movimentoHorizontal = _inputMovimentacao.x * _cameraVirtual.right + _inputMovimentacao.y * _cameraVirtual.forward;

        _movimentoHorizontal.y = 0;

        //Criar um novo vetor para calcular a movimentação final do personagem
        //Desse modo, iremos juntar o movimento horizontal e movimento vertical em um novo vetor
        //Pois, o movimento vertical e horizontal são calculados de forma diferente
        Vector3 movimentoFinal = (_movimentoHorizontal * _velocidadeMovimentacao) + (_movimentoVertical.y * Vector3.up);

        //Aplicando o movimento final no personagem, passando como parametro o movimento vetor movimentoFinal que une os vetores movimentoHorizontal com movimentoVertical
        _characterController.Move(movimentoFinal * Time.deltaTime);
    }

    //Método responsavel por calcular e aplicar a gravidade no personagem
    private void CalcularGravidade()
    {
        //Verificando se o personagem está colidindo com o chão através do componente Character Controller e da propriedade IsGrounded
        _estaNoChao = _characterController.isGrounded;

        //Verificando se o personagem esta no chão e se a velocidade no eixo vertical é menor que zero
        if(_estaNoChao == true && _movimentoVertical.y < 0)
        {
            //Zerando a velocidade no eixo Y para que ele não se movimente em direçao ao chão
            _movimentoVertical.y = 0;
        }

        //Alterando a velocidade vertical do personagem e aplicando a formula de gravidade
        _movimentoVertical.y += _gravidade * _multiplicadorGravidade * Time.deltaTime;
    }

    //Método responsavel por adicionar o comportamento de pulo do personagem
    private void Pular()
    {
        //Verificando se a tecla de pular foi pressionada e se o personagem está colidindo com o chão
        if(_podePular == true && _estaNoChao == true)
        {
            //Exibindo uma mensagem no console
            Debug.Log("Pulando!");

            //Aplicando a força de pulo no vetor de movimentação vertical do personagem
            _movimentoVertical.y = _forcaPulo;
        }
    }
}
