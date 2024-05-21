using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision){

        if(collision.gameObject.CompareTag("Target")){

            Destroy(collision.gameObject);

        }

    }

}
