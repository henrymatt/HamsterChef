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

    [SerializeField] private float findPatrolLocationFrequency = 3f;
    private float patrolTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        patrolLocations = GameObject.FindGameObjectsWithTag("PatrolLocation");
        player = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (patrolTimer <= 0f)
        {
            navMeshAgent.SetDestination(patrolLocations[Random.Range(0, patrolLocations.Length - 1)].transform.position);
            patrolTimer = findPatrolLocationFrequency;
        }
        if(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            patrolTimer -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        Vector3 toPlayerVector = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z) - transform.position;

        if (Vector3.Angle(transform.forward, toPlayerVector) <= viewAngle)
        {
            if (Physics.Raycast(transform.position, new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z) - transform.position, out hit, viewDistance, LayerMask.GetMask(new string[] { "Default", "Player" }), QueryTriggerInteraction.Ignore))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    navMeshAgent.SetDestination(player.transform.position);
                }
            }
        }
    }
}
