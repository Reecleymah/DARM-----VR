using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    
    [SerializeField] private float autoDestroyTime = 5.0f;
    private float timer = 0;
    public GameManager gameManager;

    void Start()
    {
        timer += Time.deltaTime;

        if(timer > autoDestroyTime){

            Destroy(gameObject);
        }

        // Buscar el GameManager en la escena
        gameManager = FindObjectOfType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager no encontrado en la escena.");
        }

    }

    private void OnTriggerEnter(Collider other){

        if(other.gameObject.CompareTag("Player")){

            Destroy(gameObject);
            gameManager.UpdateScore(-20);
            gameManager.UpdateHealt(-15);

        }

    }
}
