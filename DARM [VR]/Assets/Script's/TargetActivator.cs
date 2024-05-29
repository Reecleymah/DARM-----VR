using System.Collections;
using System.Collections.Generic;
using MikeNspired.UnityXRHandPoser;
using UnityEngine;

public class TargetActivator : MonoBehaviour
{
    
    public List<Target> targets; // Asigna los objetivos en el Inspector
    
    private void OnTriggerEnter(Collider other)
    {
        // Itera a través de todos los objetivos y los activa
        foreach (Target target in targets)
        {
            target.Activate();
        }
    }
}
