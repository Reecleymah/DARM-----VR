using System.Collections;
using System.Collections.Generic;
using MikeNspired.UnityXRHandPoser;
using UnityEngine;

public class ProwlerController : MonoBehaviour, IDamageable
{
    private GameObject player;
    public float attackRange = 10.0f; // Distancia mínima para atacar al jugador
    public float attackRate = 1.5f; // Tiempo en segundos entre ataques
    private float nextAttackTime;
    private Animator animator; // Referencia al componente Animator
    private AudioSource audioSource;
    public AudioClip attackSound; // Sonido del ataque
    public bool death = false; // Variable para indicar si el enemigo está muerto

    void Start()
    {
        player = GameObject.Find("Point"); // Busca al jugador en la jerarquía de objetos por su nombre
        animator = GetComponent<Animator>(); // Obtén el componente Animator
        audioSource = GetComponent<AudioSource>();

        // Iniciar con la animación de salto
        StartCoroutine(JumpAttackCo());
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
            
            // Mantener la mirada hacia el jugador
            transform.LookAt(player.transform.position);
            
            // Calcula la distancia hacia el jugador
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            // Si el jugador está dentro del rango de ataque y es el momento de atacar
            if (distanceToPlayer <= attackRange && Time.time > nextAttackTime)
            {
                // Realiza el ataque
                StartCoroutine(AttackPlayer());
                nextAttackTime = Time.time + attackRate;
            }
        }
    }

    public void TakeDamage(float damage, GameObject damager)
    {
        Debug.Log("TakeDamage called, setting death to true.");
        death = true; // Indicar que el enemigo está muerto
    }

    IEnumerator JumpAttackCo()
    {
        // Iniciar la animación de JumpAttack
        animator.SetTrigger("JumpAttack");
        
        // Esperar a que la animación de JumpAttack termine
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length);
    }

    IEnumerator AttackPlayer()
    {
        // Iniciar la animación de ataque
        animator.SetTrigger("Attack");

        // Esperar un pequeño retraso para sincronizar con la animación
        yield return new WaitForSeconds(0.5f);

        // Reproduce el sonido del ataque si hay un AudioSource y un sonido asignado
        if (audioSource != null && attackSound != null)
        {
            audioSource.PlayOneShot(attackSound);
        }

        // Aquí puedes agregar lógica adicional para aplicar daño al jugador, si es necesario
    }
}
