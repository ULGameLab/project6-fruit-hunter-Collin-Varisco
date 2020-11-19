using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void StartFPS(){
      SceneManager.LoadScene("SampleScene");
    }

    public void MainMenuScreen(){
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
