using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Camera vrCamera;
    [SerializeField] public TextMeshProUGUI scoreText;
    [SerializeField] public TextMeshProUGUI healthText;
    public float pointsPlayer = 0;
    public float healthPlayer = 100;

    public void Awake(){

        if(Instance != this && Instance != null){

            Destroy(this);
        }else{

            Instance = this;
        }
    }

    public void UpdateScore(int points){

        Debug.Log("Entro el disparo");
        pointsPlayer += points;
        scoreText.text = string.Format("{0}", pointsPlayer);

    }

    public void UpdateHealt(int points){

        Debug.Log("Quitar vida");
        healthPlayer += points;
        healthText.text = string.Format("{0}", healthPlayer);

    }

}
