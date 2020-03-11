using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICashTextController : MonoBehaviour
{
    TextMeshProUGUI tmp;
    PlayerController playerCtrl;

    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        playerCtrl = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<PlayerController>();

        // Get init cash
        tmp.text = playerCtrl.GetCash().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // Update cash constantly
        tmp.text = playerCtrl.GetCash().ToString();
    }
}
