using System.Collections;
using System.Collections.Generic;
using MikeNspired.UnityXRHandPoser;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable{

    private GameObject player;
    [SerializeField] public GameObject projectilePrefab;
    public float force = 450.0f;
    public float fireRate = 10.0f; // Tiempo en segundos entre disparos
    private float nextFireTime;
    private Animator animator; // Referencia al componente Animator
    private AudioSource audioSource;
    public UnityEventFloat onHit;
    
    void Start(){

        player = GameObject.Find("XR Origin"); // Busca al jugador en la jerarquía de objetos por su nombre
        animator = GetComponent<Animator>(); // Obtén el componente Animator
        audioSource = GetComponent<AudioSource>();
    }

    void Update(){

        transform.LookAt(player.transform.position);

        if (player != null && Time.time > nextFireTime){

            // Calcula la dirección hacia el jugador
            Vector3 direction = player.transform.position - transform.position;

            // Normaliza la dirección (esto hace que su longitud sea 1, lo que nos permite usarla para indicar la dirección sin afectar la distancia)
            direction.Normalize();

            animator.SetBool("IsAttack", true);
            StartCoroutine(LaunchProjectileWithDelay(2.0f, direction)); // Espera 1 segundo antes de lanzar

            // Actualiza el tiempo para el próximo disparo
            nextFireTime = Time.time + fireRate;
        }   
    }

    public void TakeDamage(float damage, GameObject damager)
    {
            onHit.Invoke(damage);
            Destroy(gameObject);
    }

    IEnumerator LaunchProjectileWithDelay(float delay, Vector3 direction){

        yield return new WaitForSeconds(delay);

        // Reproduce el sonido
        audioSource.Play();

        animator.SetBool("IsAttack", false);

        // Crea una instancia del prefab del proyectil en la posición del enemigo
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // Agrega una fuerza al proyectil en la dirección del jugador
        projectile.GetComponent<Rigidbody>().AddForce(direction * force);

    }


}
