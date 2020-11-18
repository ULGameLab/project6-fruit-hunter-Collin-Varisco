using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Count;

// ENEMY FSM STATES
public enum EnemyState { CHASE, MOVING, DEFAULT };

//[RequireComponent(typeof(NavMeshAgent))];
public class EnemyAI : MonoBehaviour
{
    GameObject player;
    GameObject enemy;
    UnityEngine.AI.NavMeshAgent agent;
    public float chaseDistance = 20.0f;

    protected EnemyState state = EnemyState.DEFAULT;
    protected Vector3 destination = new Vector3(0, 0, 0);

    AudioSource myaudio;


    // Start is called before the first frame update
    void Start()
    {
        myaudio = GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player");
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    private Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-50.0f, 50.0f), 0, Random.Range(-50.0f, 50.0f));
    }


   

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
                transform.position += (transform.position + RandomPosition());
                agent.SetDestination(destination);
                
        }
        if(other.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine(PlayAndDestroy(myaudio.clip.length));
            Destroy(gameObject);
        }
    }

    private IEnumerator PlayAndDestroy(float waitTime)
    {
        myaudio.Play();
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
        

        switch (state)
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
