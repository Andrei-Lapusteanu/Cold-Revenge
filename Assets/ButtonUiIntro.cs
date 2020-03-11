using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUiIntro : MonoBehaviour
{
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player);
    }

    public void StartGameEvent()
    {
        player.GetComponent<PlayerController>().StartGame();
    }
}
