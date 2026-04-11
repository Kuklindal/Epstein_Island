using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Patrol,
        Chase,
        Attack
    }

    public EnemyState currentState;

    public Transform[] patrolPoints;

    private int currentPoint;

    private NavMeshAgent agent;

    public Transform player;
    public float detectionRange = 15f;

    private float lostTimer;
    public float lostTime = 2f;

    public float viewAngle = 90f;

    public float flashlightBonusRange = 8f;
    private FlashlightSystem flashlight;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = EnemyState.Patrol;
        flashlight = FindObjectOfType<FlashlightSystem>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (CanSeePlayer())
        {
          currentState = EnemyState.Chase;
          lostTimer = 0f;
        }
        else
        {
          lostTimer += Time.deltaTime;

          if (lostTimer > lostTime)
          {
            currentState = EnemyState.Patrol;
          }
        }   
        StateMachine();
    }

    void StateMachine()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                IdleState();
                break;

            case EnemyState.Patrol:
                PatrolState();
                break;

            case EnemyState.Chase:
                ChaseState();
                break;

            case EnemyState.Attack:
                AttackState();
                break;
        }
    }

    void IdleState()
    {
        Debug.Log("Enemy Idle");
    }

    void PatrolState()
    {
        if (patrolPoints.Length == 0)
            return;

        agent.SetDestination(patrolPoints[currentPoint].position);

        if (Vector3.Distance(transform.position, patrolPoints[currentPoint].position) < 1f)
        {
            currentPoint++;

            if (currentPoint >= patrolPoints.Length)
                currentPoint = 0;
        }
    }

    void ChaseState()
    {
      agent.SetDestination(player.position);

      Debug.Log("Chasing player");
    }

    bool CanSeePlayer()
    {
    Vector3 directionToPlayer = (player.position - transform.position).normalized;
    Vector3 dirToEnemy = (transform.position - player.position).normalized;
    float dot = Vector3.Dot(player.forward, dirToEnemy);

    float angle = Vector3.Angle(transform.forward, directionToPlayer);

    float currentRange = detectionRange;

    // если фонарик включен → увеличиваем дальность
    if (flashlight != null && flashlight.IsOn && dot > 0.7f)
    {
        currentRange += flashlightBonusRange;
    }

    if (angle < viewAngle / 2f)
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < currentRange)
        {
            return true;
        }
    }

    return false;
    }

    void AttackState()
    {

    }
}