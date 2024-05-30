using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangesScenes : MonoBehaviour
{
    public void CambiarEscena(string NombreEscena){
        //button = GetComponent<Button>();
        StartCoroutine(action(NombreEscena));
        //Debug.Log(button.gameObject.name + " was clicked");
        //gameManagerX.StartGame(difficulty);
        //SceneManager.LoadScene(NombreEscena);
    }
    
    IEnumerator action(string NombreEscena){

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(NombreEscena);
    }
}
