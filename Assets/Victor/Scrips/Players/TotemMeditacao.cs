using UnityEngine;
using UnityEngine.Events;

public class TotemMeditacao : MonoBehaviour, IInterativo
{
    [Header("Configurações do Totem")]
    [Tooltip("A tag do jogador que tem permissão para usar este totem.")]
    public string tagPermitida = "PlayerSol";
    
    [Header("Eventos de Mundo")]
    public UnityEvent aoEntrarMundoEspiritual;
    public UnityEvent aoSairMundoEspiritual;

    private bool noMundoEspiritual = false;

    public void Interagir(GameObject quemInteragiu)
    {
        TentarInteragir(quemInteragiu);
    }

    public void TentarInteragir(GameObject playerQueInteragiu)
    {
        if (playerQueInteragiu.CompareTag(tagPermitida))
        {
            AlternarMundo();
        }
        else
        {
            Debug.Log("Este jogador não pode usar o totem!");
        }
    }

    private void AlternarMundo()
    {
        noMundoEspiritual = !noMundoEspiritual;

        if (noMundoEspiritual)
        {
            aoEntrarMundoEspiritual?.Invoke();
            Debug.Log("Player Sol entrou no Mundo Espiritual");
        }
        else
        {
            aoSairMundoEspiritual?.Invoke();
            Debug.Log("Player Sol retornou ao Mundo Físico");
        }
    }
}