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
            Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();

            _rigidbody.AddForceAtPosition(forceDirection * pushForce, transform.position, ForceMode.Impulse); //aplica movimento
        }
    }
}