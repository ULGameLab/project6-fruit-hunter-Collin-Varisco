using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void StartFPS(){ 
      PlayerPrefs.SetInt("FPSLoaded", 1);
      SceneManager.LoadScene("SampleScene");
    }

    public void StartTPS(){
      PlayerPrefs.SetInt("TPSLoaded", 1);
      SceneManager.LoadScene("ThirdPerson");
    }

    public void Restart(){
      if(PlayerPrefs.GetInt("FPSLoaded") == 1){
        StartFPS();
      }
      if(PlayerPrefs.GetInt("TPSLoaded") == 1){
        StartTPS();
      }
    }

    public void MainMenuScreen(){
      PlayerPrefs.SetInt("FPSLoaded", 0);
      PlayerPrefs.SetInt("TPSLoaded", 0);
      SceneManager.LoadScene("start");
    }

    // Start is called before the first frame update
    void Start()
    {
      Cursor.visible = true;
      Screen.lockCursor = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
