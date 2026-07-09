using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ZombieHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHP = 100f;
    private float currentHP;

    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private ZombieAI zombieAI;
    private bool isDead = false;

    // audio
    AudioSource audioSource;
    public AudioClip zombieDiesVoice;

    void Start()
    {
        currentHP = maxHP;
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        zombieAI = GetComponent<ZombieAI>(); ;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damagePoints)
    {
        if (isDead) return;

        currentHP -= damagePoints;
        Debug.Log("Zombie is damaged");

        if(currentHP <= 0)
        {
            Die();
        }else
        {
            if (animator != null) animator.SetTrigger("TakeDamage");
        }

        StartCoroutine(StopWhileIsDamaged());

    }

    IEnumerator StopWhileIsDamaged()
    {
        if(navMeshAgent != null && zombieAI != null)
        {
            zombieAI.enabled = false;
            navMeshAgent.isStopped = true;
            navMeshAgent.velocity = Vector3.zero;
        }
        yield return new WaitForSeconds(0.3f);

        if (!isDead && navMeshAgent != null && zombieAI != null)
        {
            zombieAI.enabled = true;
            navMeshAgent.isStopped = false;
        }
    }

    void Die()
    {

        isDead = true;
        Debug.Log("Zombie is Dead");

        if (zombieAI != null) zombieAI.enabled = false;
        if (navMeshAgent != null) navMeshAgent.enabled = false;

        Collider zombieCollider = GetComponent<Collider>();
        if (zombieCollider != null) zombieCollider.enabled = false;

        if (animator != null) animator.SetTrigger("Die");
        
        if(audioSource != null)
        {
            if(zombieDiesVoice != null)
            {
                audioSource.PlayOneShot(zombieDiesVoice);
            }
        }

        Destroy(gameObject, 10f);
    }
}
