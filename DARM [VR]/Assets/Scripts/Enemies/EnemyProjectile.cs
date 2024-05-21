using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    
    [SerializeField] private float autoDestroyTime = 5.0f;
    private float timer = 0;

    void Start()
    {
        timer += Time.deltaTime;

        if(timer > autoDestroyTime){

            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision){

        if(collision.gameObject.CompareTag("Player")){

            Destroy(gameObject);
            //GameManager.Instance.UpdateScore(-5.0f);

        }

    }
}
