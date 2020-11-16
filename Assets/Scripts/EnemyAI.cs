using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// ENEMY FSM STATES
public enum EnemyState { CHASE, MOVING, DEFAULT };

//[RequireComponent(typeof(NavMeshAgent))];
public class EnemyAI : MonoBehaviour
{
    GameObject player;
    UnityEngine.AI.NavMeshAgent agent;
    public float chaseDistance = 20.0f;

    protected EnemyState state = EnemyState.DEFAULT;
    protected Vector3 destination = new Vector3(0, 0, 0);



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    private Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-50.0f, 50.0f), 0, Random.Range(-50.0f, 50.0f));
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Vector3.Distance(transform.position, player.transform.position) < chaseDistance)
        {
            agent.SetDestination(player.transform.position);
        } */

        switch(state)
        {
            case EnemyState.DEFAULT:
                destination = transform.position + RandomPosition();
                if (Vector3.Distance(transform.position, player.transform.position) < chaseDistance) {
                    state = EnemyState.CHASE;
                }
                else
                {
                    state = EnemyState.MOVING;
                    agent.SetDestination(destination);
                }
                break;
            case EnemyState.MOVING:
                Debug.Log("Dest = " + destination);
                if(Vector3.Distance(transform.position, destination) < 5)
                {
                    state = EnemyState.DEFAULT;
                }

                if(Vector3.Distance(transform.position, player.transform.position) < chaseDistance)
                {
                    state = EnemyState.CHASE;
                }
                break;
            case EnemyState.CHASE:
                if(Vector3.Distance(transform.position, player.transform.position) > chaseDistance)
                {
                    state = EnemyState.DEFAULT;
                }
                agent.SetDestination(player.transform.position);
                break;
            default:
                break;
        }
    }
}
