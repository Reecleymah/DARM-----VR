using System.Collections;
using System.Collections.Generic;
using MikeNspired.UnityXRHandPoser;
using UnityEngine;

public class ArchvileController : MonoBehaviour, IDamageable
{
    private GameObject player;
    [SerializeField] public GameObject projectilePrefab;
    public float force = 450.0f;
    public float fireRate = 10.0f; // Tiempo en segundos entre disparos
    private float nextFireTime;
    private Animator animator; // Referencia al componente Animator
    private AudioSource audioSource;
    public UnityEventFloat onHit;

    public bool death = false; // Variable para indicar si el enemigo está muerto

    // Transform para especificar el punto de origen del proyectil (por ejemplo, la mano)
    [SerializeField] private Transform projectileOrigin;

    void Start()
    {
        player = GameObject.Find("Point"); // Busca al jugador en la jerarquía de objetos por su nombre
        animator = GetComponent<Animator>(); // Obtén el componente Animator
        audioSource = GetComponent<AudioSource>();

        // Asegúrate de que projectileOrigin está asignado
        if (projectileOrigin == null)
        {
            Debug.LogError("Projectile Origin is not assigned in the Inspector.");
        }
    }

    void Update()
    {
        if (death)
        {
            // Si el enemigo está muerto, no realizar ninguna acción adicional
            return;
        }

        if (player != null)
        {
            Debug.Log("Si econtro al jugador");
            
            // Mantener la mirada hacia el jugador
            transform.LookAt(player.transform.position);

            if (Time.time > nextFireTime)
            {
                // Calcula la dirección hacia el jugador
                Vector3 direction = player.transform.position - transform.position;

                // Normaliza la dirección (esto hace que su longitud sea 1, lo que nos permite usarla para indicar la dirección sin afectar la distancia)
                direction.Normalize();

                animator.SetBool("IsAttack", true);
                StartCoroutine(LaunchProjectileWithDelay(2.25f, direction)); // Espera 2 segundos antes de lanzar

                // Actualiza el tiempo para el próximo disparo
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    public void TakeDamage(float damage, GameObject damager)
    {
        Debug.Log("TakeDamage called, setting death to true.");
        // Temporarily comment out onHit.Invoke to check if it's causing issues
        onHit.Invoke(damage);
        death = true; // Indicar que el enemigo está muerto
    }

    IEnumerator LaunchProjectileWithDelay(float delay, Vector3 direction)
    {
        yield return new WaitForSeconds(delay);

        // Reproduce el sonido
        audioSource.Play();

        animator.SetBool("IsAttack", false);

        // Verifica que projectileOrigin esté asignado
        Vector3 projectileSpawnPosition = transform.position;
        if (projectileOrigin != null)
        {
            projectileSpawnPosition = projectileOrigin.position;
        }

        // Crea una instancia del prefab del proyectil en la posición del origen especificado
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPosition, Quaternion.identity);

        // Agrega una fuerza al proyectil en la dirección del jugador
        projectile.GetComponent<Rigidbody>().AddForce(direction * force);
    }
}
