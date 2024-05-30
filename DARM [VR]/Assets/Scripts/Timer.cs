using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public GameManager gameManager;
    public TextMeshProUGUI timetext;

    void Start(){
        
        // Buscar el GameManager en la escena
        gameManager = FindObjectOfType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager no encontrado en la escena.");
        }

        //timetext.text = gameManager.healthPlayer.ToString();

    }


    // Update is called once per frame
    void Update(){

        //Debug.Log(gameManager.healthPlayer);
    }
    
    IEnumerator action(string NombreEscena){

        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(NombreEscena);
    }

}
