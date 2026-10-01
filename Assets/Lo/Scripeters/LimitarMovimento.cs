using UnityEngine;

public class LimitarMovimento : MonoBehaviour
{
    [SerializeField] private float minX = -10f;
    [SerializeField] private float maxX = 10f;
    [SerializeField] private float minZ = -10f;
    [SerializeField] private float maxZ = 10f;

    private Vector3 posicaoInicial;

    void Start()
    {
        posicaoInicial = transform.position;
    }

    void Update()
    {
        Vector3 posicaoAtual = transform.position;

        float limiteMinimoX = posicaoInicial.x + minX;
        float limiteMaximoX = posicaoInicial.x + maxX;
        float limiteMinimoZ = posicaoInicial.z + minZ;
        float limiteMaximoZ = posicaoInicial.z + maxZ;

        posicaoAtual.x = Mathf.Clamp(posicaoAtual.x, limiteMinimoX, limiteMaximoX);
        posicaoAtual.z = Mathf.Clamp(posicaoAtual.z, limiteMinimoZ, limiteMaximoZ);

        transform.position = posicaoAtual;
    }
}
