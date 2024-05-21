using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Camera ARCamera;
    [SerializeField] private TextMeshProUGUI scoreText;

    public int activeEnemyCount = 0; // Contador de enemigos activos
    private float currentScore = 0;

    public void Awake(){

        if(Instance != this && Instance != null){

            Destroy(this);
        }else{

            Instance = this;
        }

    }

    public void UpdateScore(float points){

        currentScore += points;
        scoreText.text = string.Format("{0}", currentScore);

    }

    public void UpdateActiveEnemyCount(){
        activeEnemyCount++;
    }

    public void DecreseActiveEnemyCount(){
        activeEnemyCount--;
    }
    
}
