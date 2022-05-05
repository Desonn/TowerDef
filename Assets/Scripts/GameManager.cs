using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    bool ispaused;
    bool fast;
    public static Action Restart;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame


    public void RestartLevel()
    {
        SceneManager.LoadScene(1);
        Restart?.Invoke();
    }

    public void Pause()
    {
        if(ispaused == false)
        {
            Time.timeScale = 0;
            ispaused = true;
        }
       
    }

    public void ResumeGame()
    {
        if (ispaused == true|| fast == true)
        {
            Time.timeScale = 1;
            ispaused = false;
            fast = false;
        }
    }
    public void FastGame()
    {

        fast = true;
            Time.timeScale = 2;
            
        
    }




    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
}
