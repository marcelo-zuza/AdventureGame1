using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Transform playerTransform;
    private Animator animator;
    [Header("Attack Configuration")]
    [SerializeField] private float attackDistance = 2f;
    [SerializeField] private float attackIntervals = 1.5f;
    [Header("Detection Configuration")]
    [SerializeField] private float detectionRange = 15f;
    [SerializeField] private float stopChasingDistance = 15f;
    private bool playerDetected = false;
    private float cronometroAttack;
    // player variables
    private PlayerHealth playerHealth;
    private GameObject player;
    



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth.damageEffect != null) playerHealth.damageEffect.gameObject.SetActive(false);

        if (player != null)
        {
            playerTransform = player.transform;
        }else
        {
            Debug.Log("Player Não encotrado");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform == null)
        {
            SearchPlayer();
        }

        CheckPlayerDetection();

        if(playerDetected)
        {
            ChasePlayer();
        }else
        {
            StopChasing();
        }

        AnimatorUpdater();   
    }

    void CheckPlayerDetection()
    {
        if(playerTransform != null)
        {
            float distanceFromPlayer = Vector3.Distance(transform.position, playerTransform.position);

            if(distanceFromPlayer <= detectionRange)
            {
                playerDetected = true;
            }else if(distanceFromPlayer > stopChasingDistance)
            {
                playerDetected = false;
            }
        }
    }

    void ChasePlayer()
    {
        if (playerTransform != null)
        {
            navMeshAgent.SetDestination(playerTransform.position);
        }
    }

    void SearchPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            playerTransform = player.transform;
        }
    }

    void StopChasing()
    {
        if(navMeshAgent.enabled)
        {
            navMeshAgent.isStopped = true;
            navMeshAgent.velocity = Vector3.zero;
        }
    }

    void AnimatorUpdater()
    {
        if(animator != null && navMeshAgent != null)
        {
            float currentSpeed = navMeshAgent.velocity.magnitude;
            animator.SetFloat("Speed", currentSpeed);
        }

        if (playerTransform == null) return;

        float distanceFromPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if(cronometroAttack > 0)
        {
            cronometroAttack -= Time.deltaTime;
        }

        // Logic for control not walk while attack
        bool isAttacking = false;
        if(animator !=  null)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            isAttacking = stateInfo.IsName("Attack");
        }

        if(distanceFromPlayer <= attackDistance)
        {
            if(cronometroAttack <= 0)
            {
                Attack();
                StartCoroutine(DamagePlayer());
                navMeshAgent.isStopped = true;
                navMeshAgent.velocity = Vector3.zero;
            }
        }else
        {
            if (!isAttacking && playerDetected)
            {
                navMeshAgent.isStopped = false;
                ChasePlayer();
            }else
            {
                navMeshAgent.isStopped = true;
                navMeshAgent.velocity = Vector3.zero;
            }
        }
    }

    void Attack()
    {
        cronometroAttack = attackIntervals;
        Vector3 lookAtPlayer = (playerTransform.position - transform.position).normalized;
        lookAtPlayer.y = 0; // evita que o zumbi se incline para cima ou para baixo
        transform.rotation = Quaternion.LookRotation(lookAtPlayer);

        if(animator != null)
        {
            animator.SetTrigger("Attack");
        }
    }

    IEnumerator DamagePlayer()
    {
        playerHealth.playerHealth -= 5f;
        if (playerHealth.damageEffect != null) playerHealth.damageEffect.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        if (playerHealth.damageEffect != null) playerHealth.damageEffect.gameObject.SetActive(false);
    }
}
