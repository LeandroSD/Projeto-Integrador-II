using UnityEngine;

public class ObstaclePullPush : MonoBehaviour
{
    [SerializeField] private float interactRange = 1f; // range de interação com o objeto
    [SerializeField] private float pushForce; // força que empurra a caixa
    [SerializeField] private LayerMask pushableLayer; // layer que os objetos podem ser puxados
    [SerializeField] private Transform grabPoint; // ponto transform em que o objeto vai grudar

    private ConfigurableJoint activeJoint;
    private Rigidbody targetRigidbody;
    private bool isGrabbing = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isGrabbing)
            {
                TryGrabObject();
            }
        }
        if (!Input.GetKey(KeyCode.E))
        {
            ReleaseObject();
        }
    }

    private void TryGrabObject() // gruda o objeto no grabPoint se as condições forem cumpridas
    {
        RaycastHit hit; // raycast que detecta objetos na frente do player
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactRange, pushableLayer))
        {
            targetRigidbody = hit.collider.GetComponent<Rigidbody>(); // pega o rigidbody do objeto detectado
            if (targetRigidbody != null)
            {
                isGrabbing = true;

                activeJoint = gameObject.AddComponent<ConfigurableJoint>();
                activeJoint.connectedBody = targetRigidbody;

                activeJoint.xMotion = ConfigurableJointMotion.Locked; // trava movimento do objeto
                activeJoint.yMotion = ConfigurableJointMotion.Locked;
                activeJoint.zMotion = ConfigurableJointMotion.Locked;

                activeJoint.angularXMotion = ConfigurableJointMotion.Free; // trava rotação do objeto
                activeJoint.angularYMotion = ConfigurableJointMotion.Free;
                activeJoint.angularZMotion = ConfigurableJointMotion.Free;
                
                activeJoint.autoConfigureConnectedAnchor = true;
            }
        }
    }

    private void ReleaseObject() // soltar o objeto
    {
        if (activeJoint != null)
        {
            Destroy(activeJoint); // destroi a Joint entre o player e o objeto
        }
        
        targetRigidbody = null; //esquece o ultimo objeto puxado e reseta o estado da ação para false
        isGrabbing = false;
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody _rigidbody = hit.collider.attachedRigidbody; //detecta o rigidbody do objeto colidido

        if (!Input.GetKey(KeyCode.E)) //nao roda o codigo se E não tiver pressionado
        {
            return;
        }
        if (hit.normal.y > 0.7f) //verifica se o objeto é colidido pelos lados para evitar empurrar quando ta em cima dele
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

        _rigidbody.AddForce(forceDirection * pushForce, ForceMode.Impulse); // aplica o movimento final
        }
    }
}
