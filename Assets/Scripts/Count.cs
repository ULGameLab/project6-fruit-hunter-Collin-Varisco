using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Count : MonoBehaviour
{
    private int PLAYER_HEALTH = 100;
    private int current_Fruit = 0;
    private int total_Fruit = 6;
    private bool infinite_Health = false;
    public Text countText;
    public Text countFruit;
    public bool collision = false;
    public float power_time = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        countText.text = "HEALTH: " + PLAYER_HEALTH.ToString();
        countFruit.text = "FRUIT: " + current_Fruit.ToString() + "/6";
    }

    // Update is called once per frame
    void Update()
    {
       if(PLAYER_HEALTH <= 0)
        {
            SceneManager.LoadScene("SampleScene");
        }

       if(power_time >= 5)
        {
            power_time = 0.0f;
            infinite_Health = false;
            countText.text = "HEALTH: " + PLAYER_HEALTH.ToString();
        }

       if(infinite_Health == true)
        {
            power_time += Time.deltaTime;
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(5);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fruit"))
        {
            current_Fruit += 1;
            countFruit.text = "FRUIT: " + current_Fruit.ToString() + "/" + total_Fruit.ToString();
        }
        if(other.gameObject.CompareTag("Enemy"))
        {
            if (infinite_Health == false)
            {
                PLAYER_HEALTH -= 20;
                countText.text = "HEALTH: " + PLAYER_HEALTH.ToString();
                collision = true;
            }
        }
        if(other.gameObject.CompareTag("Infinite"))
        {
            countText.text = "HEALTH: INFINITE";
            infinite_Health = true;
            
        }
    }
}
