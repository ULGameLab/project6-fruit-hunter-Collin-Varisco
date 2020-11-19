using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Count : MonoBehaviour
{
    private int PLAYER_HEALTH = 100;
    private int current_Fruit = 0;
    private int total_Fruit = 8;
    private bool infinite_Health = false;
    public Text countText;
    public Text countFruit;
    //public bool collision = false;
    public float power_time = 0.0f;

    private int tempHealth;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("HealthTotal", PLAYER_HEALTH);
        tempHealth = PlayerPrefs.GetInt("HealthTotal", PLAYER_HEALTH);
        PlayerPrefs.SetInt("Invincible", 0);
        countText.text = "HEALTH: " + PLAYER_HEALTH.ToString();
        countFruit.text = "FRUIT: " + current_Fruit.ToString() + "/8";
    }

    // Update is called once per frame
    void Update()
    {
       if(PlayerPrefs.GetInt("HealthTotal") > tempHealth || PlayerPrefs.GetInt("HealthTotal") < tempHealth){
         tempHealth = PlayerPrefs.GetInt("HealthTotal"); 
         PLAYER_HEALTH = PlayerPrefs.GetInt("HealthTotal");
         if(PlayerPrefs.GetInt("Invincible") == 0){
            countText.text = "HEALTH: " + tempHealth.ToString(); 
         }
        }
       if(PLAYER_HEALTH <= 0)
        {
        
          PlayerPrefs.SetInt("FruitCollected", current_Fruit); 
          SceneManager.LoadScene("DeathScreen");
        }

       if(power_time >= 10)
        {
            PlayerPrefs.SetInt("Invincible", 0);
            power_time = 0.0f;
            infinite_Health = false;
            countText.text = "HEALTH: " + PLAYER_HEALTH.ToString();
        }

       if(infinite_Health == true)
        {
            power_time += Time.deltaTime;
        }
       if(current_Fruit == 8){
          PlayerPrefs.SetInt("HealthTotal", PLAYER_HEALTH); 
          PlayerPrefs.SetInt("FruitCollected", current_Fruit); 
          SceneManager.LoadScene("LevelComplete"); 
       }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(5);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Apple"))
        {
          current_Fruit += 1;
          if(infinite_Health == false){
            PLAYER_HEALTH += 2;
          } 
          else {
            PLAYER_HEALTH += 4;
          }
          
        
          countFruit.text = "FRUIT: " + current_Fruit.ToString() + "/" + total_Fruit.ToString();
          countText.text = "HEALTH: " + PLAYER_HEALTH.ToString();  
        }


        if (other.gameObject.CompareTag("Orange"))
        {
          current_Fruit += 1;
          if(infinite_Health == false){
            PLAYER_HEALTH += 3;
          } 
          else {
            PLAYER_HEALTH += 6;
          }
          
        
          countFruit.text = "FRUIT: " + current_Fruit.ToString() + "/" + total_Fruit.ToString();
          countText.text = "HEALTH: " + PLAYER_HEALTH.ToString();  
        }


        if (other.gameObject.CompareTag("Donut"))
        {
          if(infinite_Health == false){
            PLAYER_HEALTH -= 4;
            countFruit.text = "FRUIT: " + current_Fruit.ToString() + "/" + total_Fruit.ToString();
            countText.text = "HEALTH: " + PLAYER_HEALTH.ToString();  
          } 
        
        }
        if(other.gameObject.CompareTag("Enemy"))
        {
            if (infinite_Health == false)
            {
                PLAYER_HEALTH -= 20;
                countText.text = "HEALTH: " + PLAYER_HEALTH.ToString();
                PlayerPrefs.SetInt("collision", 1); 
            }
        }
        if(other.gameObject.CompareTag("Infinite"))
        {
            current_Fruit += 1;
            PlayerPrefs.SetInt("Invincible", 1);
            countText.text = "HEALTH: INFINITE";
            infinite_Health = true;
            
        }
    }
}
