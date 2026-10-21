using UnityEngine;

public class ZonaMuerte : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Buscamos el script de movimientos en el objeto que tocó la trampa
        movimientos personaje = collision.GetComponent<movimientos>();

        // Si lo encuentra, ejecutamos su función de reaparición
        if (personaje != null)
        {
            personaje.Respawn();
        }
    }
}