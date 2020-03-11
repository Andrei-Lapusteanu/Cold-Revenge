using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UICostDoubleFireRate : MonoBehaviour
{
    TextMeshProUGUI tmp;
    PlayerPowerUps powerUps;

    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        powerUps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPowerUps>();
    }

    void Update()
    {
        tmp.text = powerUps.PowerDoubleFireRate.GetCost().ToString();
    }
}
