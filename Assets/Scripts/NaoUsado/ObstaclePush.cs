using UnityEngine;

public class ObstaclePush : MonoBehaviour
{
    [SerializeField] private float pushForce; //força que empurra a caixa
    
    void Start()
    {
    }
    void Update()
    {
    }
//fazer interação entre CharacterController e Rigidbody
    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody _rigidbody = hit.collider.attachedRigidbody; //detecta o rigidbody do objeto colidido

        if (!Input.GetKey(KeyCode.E)) //nao roda o codigo se E não tiver pressionado
        {
            return;
        }
        if (hit.normal.y > 0.7f) //verifica se o objeto é colidido pelos lados para evitar bug quando ta em cima dele
        {
            return;
        }

        if(_rigidbody != null) //sistema de empurrar 
        {
            Vector3 forceDirection = Vector3.zero;
            if (Mathf.Abs(hit.normal.x) > Mathf.Abs(hit.normal.z)) // compara o lado do impacto e aplica movimento só em um eixo
            {
                forceDirection.x = hit.normal.x > 0 ? -1f : 1f;
            }
            else
            {
                forceDirection.z = hit.normal.z > 0 ? -1f : 1f;
            }

        _rigidbody.AddForce(forceDirection * pushForce, ForceMode.Impulse); //aplica o movimento final
        }
    }
}