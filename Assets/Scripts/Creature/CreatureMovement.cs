using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreatureMovement : MonoBehaviour
{
    GameObject[] patrolLocations;
    GameObject player;

    NavMeshAgent navMeshAgent;

    [SerializeField] private float viewAngle = 90f;
    [SerializeField] private float viewDistance = 5f;
    [SerializeField] private float hearingDistance = 25f;
    [SerializeField] private float patrolSpeed = 3f;
    [SerializeField] private float chaseSpeed = 10f;
    [SerializeField] private float investigationSpeed = 5f;

    [SerializeField] private float findPatrolLocationFrequency = 3f;
    private float patrolTimer = 0f;

    public CreatureState creatureState = CreatureState.patrolling;

    private bool canSeePlayer = false;

    private AudioBroadcastListener listener;
    private BroadcastedSound soundBeingInvesitgated;

    // Start is called before the first frame update
    void Start()
    {
        patrolLocations = GameObject.FindGameObjectsWithTag("PatrolLocation");
        player = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = patrolSpeed;
        listener = GetComponent<AudioBroadcastListener>();
    }

    // Update is called once per frame
    void Update()
    {
        if (patrolTimer <= 0f && creatureState == CreatureState.patrolling)
        {
            navMeshAgent.SetDestination(patrolLocations[Random.Range(0, patrolLocations.Length - 1)].transform.position);
            patrolTimer = findPatrolLocationFrequency;
        }

        BroadcastedSound closestSound = listener.GetClosestSound();
        if (closestSound != null)
        {
            if (soundBeingInvesitgated == null)
            {
                soundBeingInvesitgated = closestSound;
                creatureState = CreatureState.investigating;
                navMeshAgent.SetDestination(soundBeingInvesitgated.origin);
            }
            else if (soundBeingInvesitgated.GetSoundImportance(transform.position) < closestSound.GetSoundImportance(transform.position))
            {
                soundBeingInvesitgated = closestSound;
                creatureState = CreatureState.investigating;
                navMeshAgent.SetDestination(soundBeingInvesitgated.origin);
            }
            else
            {
                //nothing
            }
        }

        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            if (creatureState == CreatureState.patrolling || creatureState == CreatureState.investigating)
                patrolTimer -= Time.deltaTime;
            else if (creatureState == CreatureState.chasing && !canSeePlayer)
            {
                navMeshAgent.speed = patrolSpeed;

                EventHandler.CallDidCreatureStopChasingEvent();

                creatureState = CreatureState.patrolling;
            }
            else if (creatureState == CreatureState.investigating)
            {
                soundBeingInvesitgated = null;
            }
        }
    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        Vector3 toPlayerVector = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z) - transform.position;

        if (Vector3.Angle(transform.forward, toPlayerVector) <= viewAngle)
        {
            if (Physics.Raycast(transform.position, new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z) - transform.position, out hit, viewDistance, LayerMask.GetMask(new string[] { "Default", "Player" }), QueryTriggerInteraction.Ignore))
            {
                if (canSeePlayer = hit.collider.CompareTag("Player"))
                {
                    navMeshAgent.SetDestination(player.transform.position);
                    EventHandler.CallDidCreatureBeginChasingEvent();

                    creatureState = CreatureState.chasing;
                    navMeshAgent.speed = chaseSpeed;
                    
                }
            }
        }
    }
}

public enum CreatureState
{
    patrolling,
    investigating,
    chasing
}
