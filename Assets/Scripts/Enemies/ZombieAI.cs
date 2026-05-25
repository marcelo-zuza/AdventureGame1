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
    private float cronometroAttack;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if(player != null)
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

        ChasePlayer();
        AnimatorUpdater();   
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

    void AnimatorUpdater()
    {
        if(animator != null && navMeshAgent != null)
        {
            float currentSpeed = navMeshAgent.velocity.magnitude;
            animator.SetFloat("Speed", currentSpeed);
        }

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
            }
        }else
        {
            if (!isAttacking)
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
}
