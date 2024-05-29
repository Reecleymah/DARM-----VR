using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MultiDissolveController : MonoBehaviour
{
    public Animator animator;
    public SkinnedMeshRenderer[] skinnedMeshRenderers;
    public VisualEffect VFXGraph;
    public float dissolveRate = 0.05f;
    public float refreshRate = 0.1f;
    public float dieDelay = 0.25f;

    public AudioSource audioSource;
    public AudioClip deathSound;
    public ParticleSystem[] particleSystems;

    private bool alive = true;
    private Material[][] dissolveMaterials;

    private EnemyController enemyController;

    void Start()
    {
        if (VFXGraph != null)
        {
            VFXGraph.Stop();
            VFXGraph.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("No VFX Graph assigned in the inspector.");
        }

        if (skinnedMeshRenderers != null && skinnedMeshRenderers.Length > 0)
        {
            dissolveMaterials = new Material[skinnedMeshRenderers.Length][];
            for (int i = 0; i < skinnedMeshRenderers.Length; i++)
            {
                dissolveMaterials[i] = skinnedMeshRenderers[i].materials;
            }
        }
        else
        {
            Debug.Log("No Skinned Mesh Renderers assigned in the inspector.");
        }

        enemyController = GetComponent<EnemyController>();
        if (enemyController == null)
        {
            Debug.Log("No EnemyController found on the GameObject.");
        }
    }

    private void Update()
    {
        if (alive && enemyController != null && enemyController.death)
        {
            Debug.Log("Death detected, starting DissolveCo.");
            StartCoroutine(DissolveCo());
        }
    }

    IEnumerator DissolveCo()
    {
        alive = false;

        // Trigger the death animation
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }
        else
        {
            Debug.Log("No Animator assigned in the inspector.");
            alive = true;
            yield break;
        }

        // Play the death sound and stop other audio
        if (audioSource != null && deathSound != null)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(deathSound);
        }
        else
        {
            Debug.Log("No AudioSource or DeathSound assigned in the inspector.");
        }

        // Stop and disable particle systems
        if (particleSystems != null && particleSystems.Length > 0)
        {
            foreach (ParticleSystem ps in particleSystems)
            {
                ps.Stop();
                ps.gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.Log("No ParticleSystems assigned in the inspector.");
        }

        yield return new WaitForSeconds(dieDelay);

        // Play the VFX graph
        if (VFXGraph != null)
        {
            VFXGraph.gameObject.SetActive(true);
            VFXGraph.Play();
        }
        else
        {
            Debug.Log("No VFX Graph assigned in the inspector.");
        }

        float counter = 0;

        // Dissolve materials
        if (dissolveMaterials.Length > 0)
        {
            while (dissolveMaterials[0][0].GetFloat("DissolveAmount_") < 1)
            {
                counter += dissolveRate;
                for (int i = 0; i < dissolveMaterials.Length; i++)
                {
                    for (int j = 0; j < dissolveMaterials[i].Length; j++)
                    {
                        dissolveMaterials[i][j].SetFloat("DissolveAmount_", counter);
                    }
                }
                yield return new WaitForSeconds(refreshRate);
            }
        }
        else
        {
            Debug.Log("No Dissolving Material assigned to models.");
            yield break;
        }

        // Destroy the game object after dissolving
        Debug.Log("Dissolve complete, destroying gameObject.");
        Destroy(gameObject);
    }

}
