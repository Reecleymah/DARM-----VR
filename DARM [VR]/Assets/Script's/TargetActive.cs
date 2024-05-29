using System.Collections;
using System.Collections.Generic;
using MikeNspired.UnityXRHandPoser;
using UnityEngine;

public class TargetActive : MonoBehaviour
{
    public Target target; // Asigna el objetivo en el Inspector o encuentra la referencia en el código.

    void Start()
    {
        // Activa el objetivo cuando el juego comienza.
        target.Activate();
    }

    void Update()
    {
        // Activa el objetivo cuando se presiona la tecla "A".
        if (Input.GetKeyDown(KeyCode.A))
        {
            target.Activate();
        }

        // Simula un golpe en el objetivo cuando se presiona la tecla "H".
        if (Input.GetKeyDown(KeyCode.H))
        {
            target.TestHit();
        }
    }
}
