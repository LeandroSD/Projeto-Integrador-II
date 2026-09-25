using UnityEngine;

public class CaixaArrastavel : MonoBehaviour, IInterativo
{
    [Header("Configuração de Dados")]
    [Tooltip("Arraste o arquivo AtributosDeObjetoSO desta caixa aqui")]
    public AtributosDeObjetoSO atributos;

    public void Interagir(GameObject quemInteragiu)
    {
        InteracaoPlayer player = quemInteragiu.GetComponent<InteracaoPlayer>();
        
        if (player != null)
        {
            // Proteção para evitar erros caso esqueça de colocar o SO ou o som
            if (atributos != null && atributos.somDeInteracao != null)
            {
                //AudioSource.PlayClipAtPoint(atributos.somDeInteracao, transform.position);
            }
            
            // Manda o player segurar esta caixa
            player.SegurarObjeto(this.gameObject);
        }
    }
}