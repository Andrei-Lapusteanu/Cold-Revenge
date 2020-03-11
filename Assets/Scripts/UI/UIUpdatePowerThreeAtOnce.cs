using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIUpdatePowerThreeAtOnce : MonoBehaviour
{
    TextMeshProUGUI tmp;
    PlayerPowerUps powerUps;

    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        powerUps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPowerUps>();
    }

    // Update is called once per frame
    void Update()
    {
        tmp.text = powerUps.PowerFireThree.GetTimer().ToString();

        if (powerUps.PowerFireThree.IsActive() == false)
            Destroy(transform.parent.gameObject);
    }
}
