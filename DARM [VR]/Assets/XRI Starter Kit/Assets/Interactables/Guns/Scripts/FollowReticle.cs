using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowReticle : MonoBehaviour
{
     public Transform vrCamera;
    public Transform weapon;
    public float maxDistance = 10f; // La distancia máxima a la que se mostrará la retícula.

    private RectTransform reticleTransform;

    void Start()
    {
        reticleTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 rayDirection = weapon.forward;

        if (Physics.Raycast(weapon.position, rayDirection, out hit, maxDistance))
        {
            Vector3 hitPosition = hit.point;
            reticleTransform.position = hitPosition;
            reticleTransform.LookAt(vrCamera); // Asegúrate de que la retícula siempre mire hacia la cámara
        }
        else
        {
            reticleTransform.position = weapon.position + rayDirection * maxDistance;
            reticleTransform.LookAt(vrCamera);
        }
    }
}
