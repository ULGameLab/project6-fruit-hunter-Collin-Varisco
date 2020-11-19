using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameData : MonoBehaviour
{
    private int FruitCount;
    private int HealthCount;
    public Text FruitText;
    public Text HealthText;
  


    // Start is called before the first frame update
    void Start()
    {
      HealthCount = PlayerPrefs.GetInt("HealthTotal");
      FruitCount = PlayerPrefs.GetInt("FruitCollected");
      FruitText.text = "FRUIT: " + FruitCount.ToString() + "/8";     
      HealthText.text = "HEALTH: " + HealthCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
