using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class cambiadorEscena : MonoBehaviour
{
    
    public string NombreEscena; // Nombre de la escena a cargar

    // Este método se llama cuando el collider de este objeto entra en contacto con otro collider
    private void OnTriggerEnter(Collider other)
    {
        // Puedes agregar una condición aquí para verificar qué objeto está colisionando
        if (other.CompareTag("Player")) { // por ejemplo, si solo debe reaccionar al jugador
            StartCoroutine(CambiarEscena(NombreEscena));
        }
    }

    // Corrutina para esperar un segundo antes de cambiar de escena
    IEnumerator CambiarEscena(string NombreEscena)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(NombreEscena);
    }
}
