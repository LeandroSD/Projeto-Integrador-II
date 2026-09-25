using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GerenciadorTochas : MonoBehaviour
{
    [Header("Configuração do Puzzle")]
    public DadosDoPuzzleSO gabarito;
    
    [Header("Referências")]
    public List<TochaBlocagem> todasAsTochas; 
    
    [Header("Eventos")]
    public UnityEvent aoCompletarPuzzle;
    public UnityEvent aoErrarSequencia;

    private int indiceAtual = 0;

    private void OnEnable()
    {
        // Inscreve o gerenciador no evento de cada tocha
        foreach (TochaBlocagem tocha in todasAsTochas)
        {
            tocha.AoSerAcesa += ValidarTochaAcesa;
        }
    }

    private void OnDisable()
    {
        foreach (TochaBlocagem tocha in todasAsTochas)
        {
            tocha.AoSerAcesa -= ValidarTochaAcesa;
        }
    }

    private void ValidarTochaAcesa(TochaBlocagem tochaAcesa)
    {
        // Proteção: verifica se o gabarito foi atribuído no Inspector
        if (gabarito == null)
        {
            Debug.LogError("O ScriptableObject de Gabarito não foi colocado no Inspector!");
            return;
        }

        // Verifica se o símbolo da tocha acesa bate com o próximo símbolo esperado no gabarito
        if (tochaAcesa.simboloDaTocha.ToUpper() == gabarito.sequenciaCorreta[indiceAtual].ToUpper())
        {
            indiceAtual++; // Acertou, avança para o próximo
            Debug.Log($"Correto! Faltam {gabarito.sequenciaCorreta.Count - indiceAtual}");

            // Verifica se chegou ao fim da sequência
            if (indiceAtual >= gabarito.sequenciaCorreta.Count)
            {
                PuzzleResolvido();
            }
        }
        else
        {
            // Errou a sequência (o else agora engloba a primeira verificação)
            ResetarPuzzle();
        }
    }

    private void ResetarPuzzle()
    {
        Debug.Log("Sequência Incorreta! Resetando...");
        indiceAtual = 0;
        
        foreach (TochaBlocagem tocha in todasAsTochas)
        {
            tocha.Apagar();
        }
        
        aoErrarSequencia?.Invoke(); // Pode tocar um som de falha ou apagar o fogo bruscamente
    }

    private void PuzzleResolvido()
    {
        Debug.Log("Puzzle das Tochas Resolvido!");
        aoCompletarPuzzle?.Invoke();
    }
}