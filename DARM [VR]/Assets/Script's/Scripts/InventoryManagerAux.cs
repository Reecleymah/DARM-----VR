using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManagerAux : MonoBehaviour
{
    public GameObject reticlePrefab; // El prefab de la retícula
    public Transform vrCamera;

    // Método para sacar un arma del inventario
    public GameObject GetWeaponFromInventory(GameObject weaponPrefab, Vector3 position, Quaternion rotation)
    {
        // Instanciar el arma del prefab
        GameObject weaponClone = Instantiate(weaponPrefab, position, rotation);

        // Instanciar la retícula y establecer su padre como la cámara para mantener la posición correcta
        GameObject reticleInstance = Instantiate(reticlePrefab, vrCamera);
        reticleInstance.transform.localPosition = new Vector3(0, 0, 10); // Ajustar según sea necesario

        // Obtener el componente ReticleController del arma clonada
        FollowReticle reticleController = weaponClone.GetComponent<FollowReticle>();
        if (reticleController != null)
        {
            // Asignar las referencias necesarias
            reticleController.vrCamera = vrCamera;
            reticleController.weapon = weaponClone.transform;
        }

        return weaponClone;
    }
}
