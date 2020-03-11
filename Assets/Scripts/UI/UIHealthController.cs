using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIHealthController : MonoBehaviour
{
    TextMeshProUGUI tmp;
    PlayerController playerCtrl;

    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        playerCtrl = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<PlayerController>();

        // Get init health
        tmp.text = playerCtrl.GetInitHP().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // Update HP constantly
        tmp.text = playerCtrl.GetHealthPoints().ToString();
    }
}
