using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Count : MonoBehaviour
{
    private int PLAYER_HEALTH = 100;
    public Text countText;


    // Start is called before the first frame update
    void Start()
    {
        countText.text = "HEALTH: " + PLAYER_HEALTH.ToString();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fruit"))
        {
            PLAYER_HEALTH -= 25;
            countText.text = "Health: " + PLAYER_HEALTH.ToString();
        }
    }
}
