using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NovoGabaritoPuzzle", menuName = "Puzzles")]
public class DadosDoPuzzleSO : ScriptableObject
{
    [Tooltip("Nome apenas para organização")]
    public string nomeDoPuzzle;
    
    [Tooltip("A sequência correta")]
    public List<string> sequenciaCorreta;
}