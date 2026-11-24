using UnityEngine;

public class Inimigo : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] private int vida = 3;

    [Header("Movimento")]
    [SerializeField] private float velocidade = 2f;
    [SerializeField] private Vector2 direcao = Vector2.down; // desce na tela por padrão

    [Header("Limpeza")]
    [SerializeField] private float tempoDeVida = 10f; // some sozinho se ninguém matar

    void Start()
    {
        Destroy(gameObject, tempoDeVida);
    }

    void Update()
    {
        transform.Translate(direcao.normalized * velocidade * Time.deltaTime, Space.World);
    }

    // Chamado pelo projétil quando acerta
    public void ReceberDano(int dano)
    {
        vida -= dano;

        if (vida <= 0)
        {
            Morrer();
        }
    }

    private void Morrer()
    {
        // Aqui depois dá pra colocar explosão, som, pontuação, drop...
        Destroy(gameObject);
    }
}
