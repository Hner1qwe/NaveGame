using UnityEngine;

public class Projetil : MonoBehaviour
{
    [SerializeField] private float velocidade = 12f;
    [SerializeField] private float tempoDeVida = 3f;
    [SerializeField] private int dano = 1;

    void Start()
    {
        // Evita acumular projétil infinito na cena
        Destroy(gameObject, tempoDeVida);
    }

    void Update()
    {
        // Vector2.up no espaço local = anda na direção que o projétil está apontando.
        // Como ele nasce com a rotação da nave/pontoDeTiro, vai pra "frente" dela.
        transform.Translate(Vector2.up * velocidade * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Só reage se o que bateu tiver o script Inimigo (não precisa de tag)
        if (other.TryGetComponent(out Inimigo inimigo))
        {
            inimigo.ReceberDano(dano);
            Destroy(gameObject);
        }
    }
}
