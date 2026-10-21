using UnityEngine;
using UnityEngine.InputSystem;

public class movimientos : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float velocidad = 8f;
    public float fuerzaSalto = 12f;

    [Header("Detección de Suelo")]
    public Transform comprobadorSuelo;
    public float radioDeteccion = 0.2f;
    public LayerMask capaSuelo;

    [Header("Control")]
    public bool esControlable = true;

    // Guardamos dónde empezó el personaje para hacerle el respawn allí
    private Vector3 puntoPartida;

    private Rigidbody2D rb;
    private float movimientoHorizontal;
    private bool estaEnSuelo;
    private bool mirandoDerecha = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Memorizamos la posición inicial al iniciar la partida
        puntoPartida = transform.position;
    }

    void Update()
    {
        if (!esControlable)
        {
            movimientoHorizontal = 0f;
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            return;
        }

        // Leemos el teclado usando exclusivamente el nuevo Input System
        movimientoHorizontal = 0f;
        if (Keyboard.current != null)
        {
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            {
                movimientoHorizontal = 1f;
            }
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            {
                movimientoHorizontal = -1f;
            }
        }

        // Comprobamos si estamos tocando el suelo para permitir el salto
        if (comprobadorSuelo != null)
        {
            estaEnSuelo = Physics2D.OverlapCircle(comprobadorSuelo.position, radioDeteccion, capaSuelo);
        }
        else
        {
            estaEnSuelo = true;
        }

        // Detectamos si presionamos la barra espaciadora con el nuevo sistema
        bool pulsaSalto = Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame;

        if (pulsaSalto && estaEnSuelo)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
        }

        // Volteamos el gráfico según la dirección
        GirarSprite();
    }

    void FixedUpdate()
    {
        if (!esControlable) return;

        // Aplicamos el movimiento horizontal manteniendo la física de caída
        rb.linearVelocity = new Vector2(movimientoHorizontal * velocidad, rb.linearVelocity.y);
    }

    // Método que llaman las zonas de muerte para revivir al personaje
    public void Respawn()
    {
        rb.linearVelocity = Vector2.zero;
        transform.position = puntoPartida;
    }
    private void GirarSprite()
    {
        if ((movimientoHorizontal > 0 && !mirandoDerecha) || (movimientoHorizontal < 0 && mirandoDerecha))
        {
            mirandoDerecha = !mirandoDerecha;
            Vector3 escala = transform.localScale;
            escala.x *= -1;
            transform.localScale = escala;
        }
    }

    // Dibuja una guía visual en el editor para el radio del suelo
    private void OnDrawGizmosSelected()
    {
        if (comprobadorSuelo != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(comprobadorSuelo.position, radioDeteccion);
        }
    }
}