using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemy;
    public Vector3 spawnPoint;
    Vector3[] positions = {
                            new Vector3((505.0218f-8.226135f),(-8.02583f+8.025831f),(528.7567f+17.75531f)),
                            new Vector3(492.05137f, 0.02f, 583.6064f),
                            new Vector3(566.7508f, 0.02f, 620.6567f), 
                            new Vector3(572.7804f, 0.02f, 491.5517f),
                            new Vector3(598.7418f, 0.02f, 524.1167f),
                            new Vector3(612.9118f, 0.02f, 524.6967f),
                            new Vector3(578.0368f, 0.02f, 498.1187f),
                            new Vector3(575.0418f, 0.02f, 441.9167f),
                            new Vector3(538.9018f, 0.02f, 413.5057f)
                          };
    // Start is called before the first frame update
    void Start()
    {

              for(int i = 0; i < 9; i++){
                Instantiate(enemy, positions[i], Quaternion.identity);
              } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemy(GameObject enemyObject){
      spawnPoint = enemyObject.transform.position;
      Instantiate(enemy, positions[Random.Range(0,9)], Quaternion.identity);
    } 

}
