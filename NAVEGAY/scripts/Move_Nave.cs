using UnityEngine;

public class Move_Nave : MonoBehaviour
{
    [Header("Movimento")]
    [SerializeField] private float velocidade = 6f;

    [Header("Tiro")]
    [SerializeField] private GameObject projetilPrefab;
    [SerializeField] private Transform pontoDeTiro;   // opcional: objeto filho na ponta da nave
    [SerializeField] private float intervaloTiro = 0.2f;

    private Rigidbody2D rb;
    private Vector2 movimento;
    private float proximoTiro;

    // Controle pra bloquear diagonal
    private bool ultimoEixoFoiX;
    private float xAnterior;
    private float yAnterior;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // A / D
        float moveY = Input.GetAxisRaw("Vertical");   // W / S

        // Descobre qual eixo foi apertado por ultimo
        if (moveX != 0 && xAnterior == 0) ultimoEixoFoiX = true;
        if (moveY != 0 && yAnterior == 0) ultimoEixoFoiX = false;
        xAnterior = moveX;
        yAnterior = moveY;

        // Sem diagonal: se os dois eixos estiverem ativos, vale o ultimo apertado
        if (moveX != 0 && moveY != 0)
        {
            if (ultimoEixoFoiX) moveY = 0;
            else moveX = 0;
        }

        movimento = new Vector2(moveX, moveY);

        // Tiro: segura o botão esquerdo do mouse pra atirar continuamente
        if (Input.GetMouseButton(0) && Time.time >= proximoTiro)
        {
            Atirar();
            proximoTiro = Time.time + intervaloTiro;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movimento * velocidade;
    }

    void Atirar()
    {
        if (projetilPrefab == null) return;

        Transform origem = pontoDeTiro != null ? pontoDeTiro : transform;
        Instantiate(projetilPrefab, origem.position, origem.rotation);
    }
}
