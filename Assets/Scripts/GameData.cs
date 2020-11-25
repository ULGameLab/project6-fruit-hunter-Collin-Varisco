using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameData : MonoBehaviour
{
    private int FruitCount;
    private int PointsCount;
    public Text FruitText;
    public Text PointsText;
  


    // Start is called before the first frame update
    void Start()
    {
      PointsCount = PlayerPrefs.GetInt("Points");
      FruitCount = PlayerPrefs.GetInt("FruitCollected");
      FruitText.text = "FRUIT: " + FruitCount.ToString() + "/8";     
      PointsText.text = "POINTS: " + PointsCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
