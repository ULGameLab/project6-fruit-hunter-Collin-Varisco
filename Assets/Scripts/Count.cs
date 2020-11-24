using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class Count : MonoBehaviour
{
    private int PLAYER_HEALTH = 100;
    private int current_Fruit = 0;
    private int total_Fruit = 8;
    private int current_Points = 0;
    private bool infinite_Health = false;
    public Text countText;
    public Text countFruit;
    public Text PointsText;
    public AudioSource FruitSound;
    public AudioSource JunkSound;
    public AudioSource InfiniteSound;
    //public bool collision = false;
    public float power_time = 0.0f;
    public AudioSource takenDamage;

    private int tempHealth;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Points", current_Points);
        PlayerPrefs.SetInt("HealthTotal", PLAYER_HEALTH);
        tempHealth = PlayerPrefs.GetInt("HealthTotal", PLAYER_HEALTH);
        PlayerPrefs.SetInt("Invincible", 0);
        PointsText.text = "POINTS: " + current_Points.ToString();
        countText.text = "HEALTH: " + PLAYER_HEALTH.ToString();
        countFruit.text = "FRUIT: " + current_Fruit.ToString() + "/8";
    }

    // Update is called once per frame
    void Update()
    {
       if(PlayerPrefs.GetInt("Points") > current_Points)
        {
            current_Points = PlayerPrefs.GetInt("Points");
            PointsText.text = "POINTS: " + current_Points.ToString();
        } 
       if(PlayerPrefs.GetInt("HealthTotal") > tempHealth || PlayerPrefs.GetInt("HealthTotal") < tempHealth){
         tempHealth = PlayerPrefs.GetInt("HealthTotal"); 
         PLAYER_HEALTH = PlayerPrefs.GetInt("HealthTotal");
         if(PlayerPrefs.GetInt("Invincible") == 0){
            countText.text = "HEALTH: " + PLAYER_HEALTH.ToString(); 
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
          FruitSound.Play(); 
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
          FruitSound.Play();
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
            JunkSound.Play(); 
            PLAYER_HEALTH -= 4;
            countFruit.text = "FRUIT: " + current_Fruit.ToString() + "/" + total_Fruit.ToString();
            countText.text = "HEALTH: " + PLAYER_HEALTH.ToString();  
          } 
        
        }
        if(other.gameObject.CompareTag("Enemy"))
        {
            if (infinite_Health == false)
            {
                takenDamage.Play();
                PLAYER_HEALTH -= 10;
                countText.text = "HEALTH: " + PLAYER_HEALTH.ToString();
                PlayerPrefs.SetInt("collision", 1); 
            }
        }
        if(other.gameObject.CompareTag("Infinite"))
        {
            InfiniteSound.Play();
            current_Fruit += 1;
            PlayerPrefs.SetInt("Invincible", 1);
            countFruit.text = "FRUIT: " + current_Fruit.ToString() + "/" + total_Fruit.ToString();
            countText.text = "HEALTH: INFINITE";
            infinite_Health = true;
            
        }
    }
}
