using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuyButtonDoubleFireRate : MonoBehaviour
{
    PlayerController playerCtrl;
    PlayerPowerUps powerUps;
    Button button;

    // Start is called before the first frame update
    void Start()
    {
        playerCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        powerUps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPowerUps>();
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCtrl.GetCash() < powerUps.PowerDoubleFireRate.GetCost() || powerUps.PowerDoubleFireRate.IsActive() == true)
            button.interactable = false;
        else
            button.interactable = true;
    }
}
