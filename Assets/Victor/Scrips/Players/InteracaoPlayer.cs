using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteracaoPlayer : MonoBehaviour
{
    [Header("Configurações de Interação")]
    [Tooltip("Distância máxima para interagir com os objetos")]
    public float raioInteracao = 1.5f;
    public LayerMask layerInterativa;
    
    [Header("Sistema de Arrastar")]
    [Tooltip("Ponto De Segurar")]
    public Transform pontoDeSegurar; 

    private GameObject objetoSegurado = null;

    void Update()
    {
        // Verifica se a tecla 'E' foi pressionada usando o New Input System
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (objetoSegurado != null)
            {
                // Se já estiver segurando algo, aperta E para soltar
                SoltarObjeto();
            }
            else
            {
                // Se estiver de mãos vazias, tenta interagir com o que estiver perto
                TentarInteragir();
            }
        }
    }

    private void TentarInteragir()
    {
        // Cria uma esfera invisível em volta do player para achar objetos da layer Interativo
        Collider[] colisoresProximos = Physics.OverlapSphere(transform.position, raioInteracao, layerInterativa);
        
        if (colisoresProximos.Length > 0)
        {
            // Pega o objeto interativo mais próximo do player, não apenas o primeiro do array
            Collider colisorMaisProximo = colisoresProximos
                .OrderBy(c => Vector3.Distance(transform.position, c.transform.position))
                .First();

            GameObject objetoProximo = colisorMaisProximo.gameObject;
            
            IInterativo interativo = objetoProximo.GetComponentInParent<IInterativo>();

            if (interativo != null)
            {
                interativo.Interagir(this.gameObject);
            }
        }
    }

    public void SegurarObjeto(GameObject objeto)
    {
        if (pontoDeSegurar == null)
        {
            Debug.LogWarning("Ponto De Segurar não foi configurado no Inspector! Crie um Empty GameObject filho do Player, posicionado à frente dele, e arraste-o para esse campo.");
            return;
        }

        objetoSegurado = objeto;
        
        // Puxa o objeto para a posição do "ponto de segurar" (deve ser um objeto filho
        // deslocado à frente do Player, NÃO o próprio Transform do Player) e o transforma em filho
        objetoSegurado.transform.position = pontoDeSegurar.position;
        objetoSegurado.transform.SetParent(this.transform);
        
        // Desativa a física do objeto para ele não bugar o seu CharacterController enquanto você anda
        Rigidbody rb = objetoSegurado.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;
    }

    private void SoltarObjeto()
    {
        // Tira o objeto de dentro do Player
        objetoSegurado.transform.SetParent(null);
        
        // Devolve a física para a caixa cair ou parar no chão
        Rigidbody rb = objetoSegurado.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = false;

        objetoSegurado = null;
    }

    // Desenha uma esfera amarela no Unity (Scene) para você ajustar o tamanho do raio de interação
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, raioInteracao);
    }
}