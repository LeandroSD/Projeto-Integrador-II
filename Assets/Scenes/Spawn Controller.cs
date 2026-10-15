using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.InputSystem;

public class SpawnController : MonoBehaviour
{
    //Lista que armazena a quantidade de pontos de spawn da cena
    [SerializeField] private List<Transform> _spawnPoints;

    //Tempo de delay entre o spawn
    private float _delayToSpawn = 0.1f;

    //Contagem de jogadores - esse numero irá funcionar como index
    private int _contagemPlayer = 0;

    //Método que será invocado toda vez que um jogador entrar na partida
    //Adicionando cada jogador em um ponto de spawn diferente a partir da quantidade de jogadores
    public void OnPlayerEnterGame(PlayerInput player)
    {
        StartCoroutine(StartSpawnPlayer());

        IEnumerator StartSpawnPlayer()
        {
            //Desabilitando o character controller
            player.GetComponent<CharacterController>().enabled = false;

            //Definindo a nova posição do objeto
            Transform novaPosicao = _spawnPoints[_contagemPlayer];

            //Alterando posição e rotação do objeto
            player.transform.SetPositionAndRotation(novaPosicao.position, player.transform.rotation);

            //Adicionando em delay para habilitar o character controller
            yield return new WaitForSeconds(_delayToSpawn);

            //Habilitando character controller
            player.GetComponent<CharacterController>().enabled = true;

            //Atualizando a contagem de jogadores
            _contagemPlayer++;
        }

    }
}
