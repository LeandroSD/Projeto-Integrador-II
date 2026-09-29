using UnityEngine;

// Cria o menu de atalho para gerar esse arquivo direto na Unity
[CreateAssetMenu(fileName = "NovoAtributoDeObjeto", menuName = "Interacao/Atributos de Objeto")]
public class AtributosDeObjetoSO : ScriptableObject
{
    [Header("Interface de Usuário (UI)")]
    [Tooltip("Texto que aparece na tela. Ex: 'Acender Tocha', 'Empurrar Caixa'")]
    public string nomeParaUI = "Interagir";
    
    [Header("Física e Movimento")]
    [Tooltip("Define se o jogador pode pegar este objeto ou apenas ativar")]
    public bool podeSerArrastado = false;
    
    [Tooltip("Afeta a velocidade do jogador. Ex: 10 para barril, 50 para engrenagem gigante")]
    public float peso = 0f;

    [Header("Áudio (Feedback)")]
    [Tooltip("Som que toca ao iniciar a interação (madeira arrastando, fogo acendendo, etc)")]
    public AudioClip somDeInteracao;
}