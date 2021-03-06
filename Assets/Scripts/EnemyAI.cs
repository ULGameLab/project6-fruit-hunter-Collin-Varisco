﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;
using static Count;

// ENEMY FSM STATES
public enum EnemyState { CHASE, MOVING, DEFAULT };

//[RequireComponent(typeof(NavMeshAgent))];
public class EnemyAI : MonoBehaviour
{
    ParticleSystem explosion;
    bool explosionStarted = false;

    GameObject player;
    UnityEngine.AI.NavMeshAgent agent;
    public float chaseDistance = 20.0f;
    protected EnemyState state = EnemyState.DEFAULT;
    protected Vector3 destination = new Vector3(0, 0, 0);
    public AudioSource myaudio;
    float idleTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        explosion = transform.GetComponent<ParticleSystem>();
        myaudio = GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player");
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    private Vector3 RandomPosition()
    {
        return new Vector3(transform.position.x+Random.Range(-5.0f, 5.0f), 0, transform.position.y + Random.Range(-5.0f, 5.0f));
    }


    void destroyEnemy(){
      GameManager gameManagerReference = GameObject.FindObjectOfType<GameManager>();
      gameManagerReference.SpawnEnemy(gameObject);
      
      Destroy(gameObject);

    } 

    private void StartExplosion()
    {
        if(explosionStarted == false)
        {
            explosion.Play();
            explosionStarted = true;
        }
    }


    private void StopExplosion()
    {
        explosionStarted = false;
        explosion.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Vector3 hitPlayerPosition = transform.position;
            float pauseMovement = 0.0f;
            while(pauseMovement < 2.0f)
            {
                transform.position = hitPlayerPosition;
                pauseMovement += Time.deltaTime;
            }
                
        }
        if(other.gameObject.CompareTag("Bullet"))
        {
            if (PlayerPrefs.GetInt("Invincible") == 0)
            {
                PlayerPrefs.SetInt("Points", PlayerPrefs.GetInt("Points") + 10);
            } else if(PlayerPrefs.GetInt("Invincible") == 1)
            {
                PlayerPrefs.SetInt("Points", PlayerPrefs.GetInt("Points") + 20);
            }
            gameObject.GetComponent<ParticleSystemRenderer>().enabled = true;
            StartExplosion();
            StartCoroutine(PlayAndDestroy(myaudio.clip.length));
            //destroyEnemy();
        }
    }

    private IEnumerator PlayAndDestroy(float waitTime)
    {
        myaudio.Play();
        yield return new WaitForSeconds(waitTime/5);
        StopExplosion();
        GameManager gameManagerReference = GameObject.FindObjectOfType<GameManager>();
        gameManagerReference.SpawnEnemy(gameObject);
        Destroy(gameObject);
    }

    private IEnumerator idleMovement(float waitTime){
      yield return new WaitForSeconds(waitTime);
    }

    // Update is called once per frame
    void Update()
    {
        
        

        switch (state)
        {
            case EnemyState.DEFAULT:
                destination = player.transform.position;
                if (Vector3.Distance(transform.position, player.transform.position) < chaseDistance) {
                    state = EnemyState.CHASE;
                }
                break;
                /*
                else                
                {     
                    state = EnemyState.MOVING;
                    while(idleTime < 3){
                      idleTime +=Time.deltaTime;
                    }
                    agent.SetDestination(player.transform.position);
                }
                break;*/
            case EnemyState.MOVING:
                
                if(Vector3.Distance(transform.position, destination) < 3)
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
