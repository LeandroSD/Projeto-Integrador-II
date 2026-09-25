using UnityEngine;
using System;

public class TochaBlocagem : MonoBehaviour, IInterativo
{
    [Header("Configuração de Dados")]
    [Tooltip("Arraste o arquivo AtributosDeObjetoSO desta tocha aqui")]
    public AtributosDeObjetoSO atributos;

    [Header("Identificação do Puzzle")]
    [Tooltip("Coloque a letra correspondente a esta tocha (Ex: A, S, B ou D)")]
    public string simboloDaTocha; 

    private Renderer meuRenderer;
    
    // O Gerenciador precisa ler se ela está acesa
    public bool estaAcesa { get; private set; } 
    
    // O Gerenciador "escuta" este evento para saber a hora exata que a tocha ligou
    public event Action<TochaBlocagem> AoSerAcesa;

    void Start()
    {
        meuRenderer = GetComponentInChildren<Renderer>();

        if (meuRenderer == null)
        {
            Debug.LogWarning($"[{name}] Nenhum Renderer encontrado em si mesmo ou nos filhos. A tocha não vai mudar de cor.");
        }
        Apagar(); // Garante que comece apagada e na cor original
    }

    public void Interagir(GameObject quemInteragiu)
    {
        if (!estaAcesa)
        {
            estaAcesa = true;
            
            if (meuRenderer != null)
            {
                // Muda a cor base para vermelho para simular o fogo aceso
                meuRenderer.material.color = Color.red;
            }

            Debug.Log("Tocha acesa: " + simboloDaTocha);
            
            // Avisa o GerenciadorTochas que esta tocha acabou de ser ativada
            AoSerAcesa?.Invoke(this); 
        }
    }

    public void Apagar()
    {
        estaAcesa = false;
        if (meuRenderer != null)
        {
            meuRenderer.material.color = Color.white; // Volta para a cor padrão
        }
    }
}