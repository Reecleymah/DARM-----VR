using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform playerHead;
    [SerializeField] private float spawnDistance = 3f;
    [SerializeField] private float yOffset = 1f;
    [SerializeField] private GameObject panel;

    void Update()
    {
        // Calcula la posición frente al jugador
        Vector3 position = playerHead.position + playerHead.forward * spawnDistance;
        position.y += yOffset; // Ajusta la altura si es necesario
        panel.transform.position = position;

        // Rota el panel para que siempre mire al jugador
        panel.transform.LookAt(playerHead.position);
        
        //transform.position = player.transform.position + new Vector3(0f, 0f, 0.5f);
        //transform.rotation = player.transform.rotation;
    }
}
