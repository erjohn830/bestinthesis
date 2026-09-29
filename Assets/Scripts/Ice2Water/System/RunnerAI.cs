using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class RunnerAI : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;

    public Transform player;
    public Transform[] patrolPoints;
    public Transform[] hidingSpots;

    public float detectRange = 8f;
    public float panicRange = 4f;

    public float baseSpeed = 3.5f;
    public float panicSpeed = 6f;

    public bool isFrozen = false;

    int currentPatrol;
    float rescueThinkTimer = 0f;

    RunnerAI frozenTeammate;
    public static List<RunnerAI> allRunners = new List<RunnerAI>();
    GameFlow gameFlow; 

    void Awake()
    {
        allRunners.Add(this);
    }

    void OnDestroy()
    {
        allRunners.Remove(this);
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        agent.speed = baseSpeed;
        gameFlow = FindObjectOfType<GameFlow>(); 

        GoToNextPatrol();
    }

    void Update()
    {
        if (isFrozen) return;
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < panicRange)
        {
            EscapePlayer();
        }
        else if (distance < detectRange)
        {
            Hide();
        }
        else
        {
            Patrol();
        }
        LookForFrozenTeammates();

        anim.SetFloat("Speed", agent.velocity.magnitude);
    }

    // PATROL SYSTEM
    void Patrol()
    {
        if (patrolPoints.Length == 0) return;
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPatrol(); // move to next patrol point
        }
    }

    void GoToNextPatrol()
    {
        if (patrolPoints.Length == 0) return;
        agent.destination = patrolPoints[currentPatrol].position;
        currentPatrol = (currentPatrol + 1) % patrolPoints.Length; // cycle patrol points
    }

    // ESCAPE PLAYER
    void EscapePlayer()
    {
        Vector3 runDirection = transform.position - player.position; // opposite direction
        Vector3 newPos = transform.position + runDirection.normalized * 6f; // run distance
        agent.speed = panicSpeed; // faster speed
        agent.SetDestination(newPos); // run away
    }

    // HIDE SYSTEM
    void Hide()
    {
        if (hidingSpots.Length == 0) return;

        Transform bestSpot = hidingSpots[Random.Range(0, hidingSpots.Length)];

        agent.SetDestination(bestSpot.position); // move to hiding spot
    }

    // FREEZE FUNCTION
    public void Freeze()
    {
        if (isFrozen) return;
        isFrozen = true;
        agent.isStopped = true;

        anim.SetTrigger("Freeze");

        if (gameFlow != null)
        {
            gameFlow.RunnerFrozen();
        }
    }

    // UNFREEZE FUNCTION
    public void Unfreeze()
    {
        isFrozen = false;
        agent.isStopped = false; // allow movement again
        anim.SetTrigger("Unfreeze"); // play unfreeze animation
    }

    // TEAM RESCUE SYSTEM
    void LookForFrozenTeammates()
    {
        if (allRunners.Count <= 1)
            return;

        if (frozenTeammate != null)
        {
            RescueTeammate();
            return;
        }

        foreach (RunnerAI runner in allRunners)
        {
            if (runner != this && runner.isFrozen)
            {
                float dist = Vector3.Distance(transform.position, runner.transform.position);

                if (dist < 10f)
                {
                    frozenTeammate = runner;
                    rescueThinkTimer = 10f; 
                    break;
                }
            }
        }
    }

    void RescueTeammate()
    {
        if (frozenTeammate == null)
            return;

        rescueThinkTimer -= Time.deltaTime;

        if (rescueThinkTimer > 0)
        {
            agent.isStopped = true; // thinking
            return;
        }

        agent.isStopped = false;
        agent.SetDestination(frozenTeammate.transform.position);

        float dist = Vector3.Distance(transform.position, frozenTeammate.transform.position);

        if (dist < 1.5f)
        {
            frozenTeammate.Unfreeze();
            frozenTeammate = null; // reset
        }
    }
}