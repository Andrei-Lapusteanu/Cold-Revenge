using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenuReturnEvent()
    {
        Time.timeScale = 1.0f;
        //Little trick
        GameObject.Find("Player").GetComponent<PlayerController>().Heal(100);
        SceneManager.LoadScene(0);
    }
}
