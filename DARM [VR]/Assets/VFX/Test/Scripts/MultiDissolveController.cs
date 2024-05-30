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
    public float dieDelay = 0.15f;

    public AudioSource audioSource;
    public AudioClip deathSound;
    public ParticleSystem[] particleSystems;

    private bool alive = true;
    private Material[][] dissolveMaterials;

    private ArchvileController archvileController;
    public int type = 0;

    public GameManager gameManager;

    void Start()
    {
        
        // Buscar el GameManager en la escena
        gameManager = FindObjectOfType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager no encontrado en la escena.");
        }
        
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


        archvileController = GetComponent<ArchvileController>();
        if (archvileController == null)
        {
            Debug.Log("No ArchvileController found on the GameObject.");
        }
    }

    private void Update()
    {
        
        if (alive && archvileController != null && archvileController.death)
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
            animator.SetBool("isDeath", true);
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

        // Wait for the animation to start
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("Idle|Death"));
        
        // Wait for the dieDelay before starting the dissolve effect
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

        // Optionally, destroy the game object after dissolving
        Debug.Log("Dissolve complete, destroying gameObject.");
        gameManager.UpdateScore(20);
        Destroy(gameObject);
    }
}
